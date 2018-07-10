﻿using System;
using System.Collections.Generic;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.Collections;
using agsXMPP.protocol.iq.roster;
using System.Threading;
using agsXMPP.protocol.x.muc;

namespace JabberClient
{
    class Program
    {

        static XmppClientConnection xmpp = null;
        static bool _wait;
        static void Main(string[] args)
        {
            /*
             * Starting Jabber Console, setting the Display settings
             * 
             */
            Console.Title = "Jabber Client";
            Console.ForegroundColor = ConsoleColor.White;


            /*
             * Login
             * 
             */
            Console.WriteLine("Login");
            Console.WriteLine();
            Console.WriteLine("JID: ");
            string JID_Sender = "bala@bala.com";
            Console.WriteLine("Password: ");
            string Password = "123456";

            /*
             * Creating the Jid and the XmppClientConnection objects
             */
            Jid jidSender = new Jid(JID_Sender);
             xmpp = new XmppClientConnection("127.0.0.1");

            /*
             * Open the connection
             * and register the OnLogin event handler
             */
            try
            {
                xmpp.Open("bala", Password);
                xmpp.OnLogin += new ObjectHandler(xmpp_OnLogin);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            /*
             * workaround, jus waiting till the login 
             * and authentication is finished
             * 
             */
            Console.Write("Wait for Login ");
            int i = 0;
            _wait = true;
            do
            {
                Console.Write(".");
                i++;
                if (i == 3)
                    _wait = false;
                Thread.Sleep(500);
            } while (_wait);
            Console.WriteLine();

            /*
             * 
             * just reading a few information
             * 
             */
            Console.WriteLine("Login Status:");
            Console.WriteLine("xmpp Connection State {0}", xmpp.XmppConnectionState);
            Console.WriteLine("xmpp Authenticated? {0}", xmpp.Authenticated);
            Console.WriteLine();

            /*
             * 
             * tell the world we are online and in chat mode
             * 
             */
            Console.WriteLine("Sending Precence");
            Presence p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            xmpp.Send(p);
            Console.WriteLine();

            /*
             * 
             * get the roster (see who's online)
             */
            xmpp.OnPresence += new PresenceHandler(xmpp_OnPresence);
             

            //wait until we received the list of available contacts            
            Console.WriteLine();
            Thread.Sleep(500);

            /*
             * now we catch the user entry, TODO: who is online
             */
            Console.WriteLine("Enter Chat Partner JID:");
            string JID_Receiver = Console.ReadLine();
            Console.WriteLine();

        
         /* Chat starts here
         */
        Console.WriteLine("Start Chat");

            /*
             * Catching incoming messages in
             * the MessageCallBack
             */
            //xmpp.MessageGrabber.Add(new Jid(JID_Receiver),
              //                       new BareJidComparer(),
             //                        new MessageCB(MessageCallBack),
              //                       null);

            xmpp.MessageGrabber.Add("",
                                     new BareJidComparer(),
                                     new MessageCB(MessageCallBack),
                                     null);



            /*
             * Sending messages
             * 
             */
            string outMessage;
            bool halt = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                outMessage = Console.ReadLine();
                if (outMessage == "q!")
                {
                    halt = true;
                }
                else
                {
                    xmpp.Send(new Message(new Jid(JID_Receiver),
                                  MessageType.chat,
                                  outMessage));
                }

            } while (!halt);
            Console.ForegroundColor = ConsoleColor.White;

            /*
             * finally we close the connection
             * 
             */
            xmpp.Close();
        }

        // Is called, if the precence of a roster contact changed        
        static void xmpp_OnPresence(object sender, Presence pres)
        {
            Console.WriteLine("Available Contacts: ");
            Console.WriteLine("{0}@{1}  {2}", pres.From.User, pres.From.Server, pres.Type);
            //Console.WriteLine(pres.From.User + "@" + pres.From.Server + "  " + pres.Type);
            Console.WriteLine();
        }

        // Is raised when login and authentication is finished 
        static void xmpp_OnLogin(object sender)
        {
            _wait = false;
            Console.WriteLine("Logged In");
        }

        //Handles incoming messages
        static void MessageCallBack(object sender,
                                    agsXMPP.protocol.client.Message msg,
                                    object data)
        {
            if (msg.Body != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}>> {1}", msg.From.User, msg.Body);
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                try
                {
                    Console.WriteLine(msg.FirstChild.GetAttributeJid("jid"));
                    MucManager _manager = new MucManager(xmpp);
                    _manager.JoinRoom(msg.FirstChild.GetAttributeJid("jid"), "test");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }

            
        }
    }
}
