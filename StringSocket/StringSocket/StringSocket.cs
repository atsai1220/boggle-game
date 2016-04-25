// Written by Joe Zachary for CS 3500, November 2012
// Revised by Joe Zachary April 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CustomNetworking
{
    /// <summary> 
    /// A StringSocket is a wrapper around a Socket.  It provides methods that
    /// asynchronously read lines of text (strings terminated by newlines) and 
    /// write strings. (As opposed to Sockets, which read and write raw bytes.)  
    ///
    /// StringSockets are thread safe.  This means that two or more threads may
    /// invoke methods on a shared StringSocket without restriction.  The
    /// StringSocket takes care of the synchronization.
    /// 
    /// Each StringSocket contains a Socket object that is provided by the client.  
    /// A StringSocket will work properly only if the client refrains from calling
    /// the contained Socket's read and write methods.
    /// 
    /// If we have an open Socket s, we can create a StringSocket by doing
    /// 
    ///    StringSocket ss = new StringSocket(s, new UTF8Encoding());
    /// 
    /// We can write a string to the StringSocket by doing
    /// 
    ///    ss.BeginSend("Hello world", callback, payload);
    ///    
    /// where callback is a SendCallback (see below) and payload is an arbitrary object.
    /// This is a non-blocking, asynchronous operation.  When the StringSocket has 
    /// successfully written the string to the underlying Socket, or failed in the 
    /// attempt, it invokes the callback.  The parameters to the callback are a
    /// (possibly null) Exception and the payload.  If the Exception is non-null, it is
    /// the Exception that caused the send attempt to fail.
    /// 
    /// We can read a string from the StringSocket by doing
    /// 
    ///     ss.BeginReceive(callback, payload)
    ///     
    /// where callback is a ReceiveCallback (see below) and payload is an arbitrary object.
    /// This is non-blocking, asynchronous operation.  When the StringSocket has read a
    /// string of text terminated by a newline character from the underlying Socket, or
    /// failed in the attempt, it invokes the callback.  The parameters to the callback are
    /// a (possibly null) string, a (possibly null) Exception, and the payload.  Either the
    /// string or the Exception will be non-null, but nor both.  If the string is non-null, 
    /// it is the requested string (with the newline removed).  If the Exception is non-null, 
    /// it is the Exception that caused the send attempt to fail.
    /// </summary>

    public class StringSocket
    {
        /// <summary>
        /// The type of delegate that is called when a send has completed.
        /// </summary>
        public delegate void SendCallback(Exception e, object payload);

        /// <summary>
        /// The type of delegate that is called when a receive has completed.
        /// </summary>
        public delegate void ReceiveCallback(String s, Exception e, object payload);

        // Underlying socket
        private Socket socket;

        private Encoding encoding;



        // Data members for send
        // Synchronizes sends
        private readonly object sendSync = new object();
        
        // Whether an asynchronous send is going on
        private bool sendOnGoing;

        // Array of bytes for sending with socket
        private byte[] outBytes;

        // Where is the socket during send
        private int byteIndex;

        // Holds callback method
        private Queue<Action<Exception>> sendCallBackQueue;

        // Store the messages to be sent.
        private Queue<string> messageQueue;

        // Determine if first time sending message from group.
        bool isStartOfGroup;



        // Data members for receive
        /// <summary>
        /// The buffer size for the char and byte buffers.
        /// </summary>
        private const int BUFFER_SIZE = 1024;

        /// <summary>
        /// Object used to synchronize access to membervariables related to recieving from the socket.
        /// </summary>
        private readonly object recieveSync = new object();

        /// <summary>
        /// True if there is a pending recieve socket callback. False otherwise.
        /// </summary>
        private bool recieveIsOngoing;

        /// <summary>
        /// Stores the incoming chars that haven't been processed yet.
        /// </summary>
        private StringBuilder incoming;

        /// <summary>
        /// Buffer to store the incomingBytes
        /// </summary>
        private byte[] incomingBytes;

        /// <summary>
        /// Buffer to store the incomingChars
        /// </summary>
        private char[] incomingChars;

        /// <summary>
        /// Object that is used to decode the incomingBytes into incomingChars.
        /// </summary>
        private Decoder decoder;

        /// <summary>
        /// Queue to store the callbacks for BeginRecieve in.
        /// </summary>
        private Queue<Action<string, Exception>> recieveCallbackQueue;

        /// <summary>
        /// Queue to store the requested lengths for BeginRecieve in.
        /// </summary>
        private Queue<int> recieveLengthQueue;

        /// <summary>
        /// The position in incoming to start searching for new line characters.
        /// </summary>
        private int start;

        /// <summary>
        /// Creates a StringSocket from a regular Socket, which should already be connected.  
        /// The read and write methods of the regular Socket must not be called after the
        /// StringSocket is created.  Otherwise, the StringSocket will not behave properly.  
        /// The encoding to use to convert between raw bytes and strings is also provided.
        /// </summary>
        public StringSocket(Socket s, Encoding e)
        {
            socket = s;
            encoding = e;

            // Initialization for send
            sendOnGoing = false;
            outBytes = new byte[0];
            byteIndex = 0;
            sendCallBackQueue = new Queue<Action<Exception>>();
            messageQueue = new Queue<string>();

            // Initialization for receive
            recieveIsOngoing = false;
            incoming = new StringBuilder();
            incomingBytes = new byte[BUFFER_SIZE];
            incomingChars = new char[BUFFER_SIZE];
            decoder = encoding.GetDecoder();
            recieveCallbackQueue = new Queue<Action<string, Exception>>();
            recieveLengthQueue = new Queue<int>();
            start = 0;
        }

        /// <summary>
        /// Shuts down and closes the socket.  No need to change this.
        /// </summary>
        public void Shutdown()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// We can write a string to a StringSocket ss by doing
        /// 
        ///    ss.BeginSend("Hello world", callback, payload);
        ///    
        /// where callback is a SendCallback (see above) and payload is an arbitrary object.
        /// This is a non-blocking, asynchronous operation.  When the StringSocket has 
        /// successfully written the string to the underlying Socket, or failed in the 
        /// attempt, it invokes the callback.  The parameters to the callback are a
        /// (possibly null) Exception and the payload.  If the Exception is non-null, it is
        /// the Exception that caused the send attempt to fail. 
        /// 
        /// This method is non-blocking.  This means that it does not wait until the string
        /// has been sent before returning.  Instead, it arranges for the string to be sent
        /// and then returns.  When the send is completed (at some time in the future), the
        /// callback is called on another thread.
        /// 
        /// This method is thread safe.  This means that multiple threads can call BeginSend
        /// on a shared socket without worrying around synchronization.  The implementation of
        /// BeginSend must take care of synchronization instead.  On a given StringSocket, each
        /// string arriving via a BeginSend method call must be sent (in its entirety) before
        /// a later arriving string can be sent.
        /// </summary>
        public void BeginSend(String s, SendCallback callback, object payload)
        {
            lock (sendSync)
            {
                messageQueue.Enqueue(s);
                //outGoingString.Append(s);

                sendCallBackQueue.Enqueue((e) => callback(e, payload));
                // If send is not on going, start one
                if (!sendOnGoing)
                {
                    // Arranges for the string to be sent, then returns
                    isStartOfGroup = true;
                    sendOnGoing = true;
                    SendBytes();
                }
            }
        }

        /// <summary>
        /// Method to actually send via socket with bytes
        /// </summary>
        private void SendBytes()
        {
            // If currently dealing with sending bytes
            if (byteIndex < outBytes.Length)
            {
                socket.BeginSend(outBytes, byteIndex, outBytes.Length - byteIndex, SocketFlags.None, SentCallBack, null);
            }
            // Not currently dealing with bytes, make another one
            else if (messageQueue.Count > 0)
            {
                if(isStartOfGroup)
                {
                    isStartOfGroup = false;
                }
                else
                {
                    var callbackMethod = sendCallBackQueue.Dequeue();
                    Task.Run(() => callbackMethod(null));
                }

                // Started from the bottom
                byteIndex = 0;
                outBytes = encoding.GetBytes(messageQueue.Dequeue());
                //outGoingString.Clear();
                socket.BeginSend(outBytes, 0, outBytes.Length, SocketFlags.None, SentCallBack, null);
            }
            else
            {
                var callbackMethod = sendCallBackQueue.Dequeue();
                Task.Run(() => callbackMethod(null));

                sendOnGoing = false;
            }

        }

        /// <summary>
        /// Callback method for when socket has successfully sent
        /// </summary>
        /// <param name="result"></param>
        private void SentCallBack(IAsyncResult result)
        {
            int bytesSent = socket.EndSend(result);
            lock (sendSync)
            {
                // Update index and try to send the rest
                if (bytesSent > 0)
                {
                    byteIndex += bytesSent;
                    SendBytes();
                }
            }
        }

        /// <summary>
        /// We can read a string from the StringSocket by doing
        /// 
        ///     ss.BeginReceive(callback, payload)
        ///     
        /// where callback is a ReceiveCallback (see above) and payload is an arbitrary object.
        /// This is non-blocking, asynchronous operation.  When the StringSocket has read a
        /// string of text terminated by a newline character from the underlying Socket, or
        /// failed in the attempt, it invokes the callback.  The parameters to the callback are
        /// a (possibly null) string, a (possibly null) Exception, and the payload.  Either the
        /// string or the Exception will be null, or possibly boh.  If the string is non-null, 
        /// it is the requested string (with the newline removed).  If the Exception is non-null, 
        /// it is the Exception that caused the send attempt to fail.  If both are null, this
        /// indicates that the sending end of the remote socket has been shut down.
        /// 
        /// This method is non-blocking.  This means that it does not wait until a line of text
        /// has been received before returning.  Instead, it arranges for a line to be received
        /// and then returns.  When the line is actually received (at some time in the future), the
        /// callback is called on another thread.
        /// 
        /// This method is thread safe.  This means that multiple threads can call BeginReceive
        /// on a shared socket without worrying around synchronization.  The implementation of
        /// BeginReceive must take care of synchronization instead.  On a given StringSocket, each
        /// arriving line of text must be passed to callbacks in the order in which the corresponding
        /// BeginReceive call arrived.
        /// 
        /// Note that it is possible for there to be incoming bytes arriving at the underlying Socket
        /// even when there are no pending callbacks.  StringSocket implementations should refrain
        /// from buffering an unbounded number of incoming bytes beyond what is required to service
        /// the pending callbacks.
        /// </summary>
        public void BeginReceive(ReceiveCallback callback, object payload, int length = 0)
        {
            lock (recieveSync)
            {
                recieveCallbackQueue.Enqueue((s, e) => callback(s, e, payload));
                recieveLengthQueue.Enqueue(length);

                if (!recieveIsOngoing)
                {
                    recieveIsOngoing = true;

                    ProcessInput();
                }
            }
        }

        /// <summary>
        /// This method reads new input and breaks it up into the requests created.
        /// </summary>
        private void ProcessInput()
        {
            while (recieveCallbackQueue.Count > 0)
            {
                string s = null;

                int length = recieveLengthQueue.First();
                if (length == 0)
                {
                    for (int i = start; i < incoming.Length; i++)
                    {
                        if (incoming[i] == '\n')
                        {
                            s = incoming.ToString(0, i);
                            if (s.Contains("\n"))
                            {
                                Console.WriteLine(s);
                            }
                            // Remove the new line character too
                            incoming.Remove(0, i + 1);
                            break;
                        }
                    }
                }
                else
                {
                    if (incoming.Length >= length)
                    {
                        s = incoming.ToString(0, length);
                        s.Remove(0, length);
                    }
                }

                if (s == null)
                {
                    start = incoming.Length;

                    break;
                }
                else
                {
                    var callback = recieveCallbackQueue.Dequeue();
                    recieveLengthQueue.Dequeue();
                    
                    Task.Run(() => callback(s, null));

                    start = 0;
                }
            }

            if (recieveCallbackQueue.Count > 0)
            {
                socket.BeginReceive(incomingBytes, 0, incomingBytes.Length, SocketFlags.None, BytesRecieved, null);
            }
            else
            {
                recieveIsOngoing = false;
            }
        }

        /// <summary>
        /// Callback called by socket. Converts bytes read into chars and appends them to incoming.
        /// Then calls ProcessInput to process them.
        /// </summary>
        /// <param name="result"></param>
        private void BytesRecieved(IAsyncResult result)
        {
            int bytesRead = socket.EndReceive(result);

            // bytesRead will be zero if the connection was closed on the other end and all bytes
            // have been read.
            if (bytesRead == 0)
            {
                socket.Close();
            }
            else
            {
                lock (recieveSync)
                {
                    int charsRead = encoding.GetDecoder().GetChars(incomingBytes, 0, bytesRead, incomingChars, 0, false);
                    incoming.Append(incomingChars, 0, charsRead);

                    ProcessInput();
                }
            }
        }
    }
}