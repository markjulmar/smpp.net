# SMPP.NET - Short Message Peer-to-Peer .NET library

SMPP is the protocol used to send and receive SMS messages over the telecom network. This library understands the SMPP protocol and can generate and receive messages when connected to the PSTN.

There are several projects:

* Smpp.NET - This is the core library which manages the protocol over TCP/IP.
* SmscGui - This is a short message service center (SMSC) sample. The SMSC is the portion of a wireless network that handles SMS operations, such as routing, forwarding and storing incoming text messages on their way to desired endpoints. This is a WinForms GUI.
* EsmeGui - This is a External Short Messaging Entities (ESME) sample which can send and receive SMS messages. This is a WinForms GUI.
* TestEsme - This is a sample (test) ESME written for the command line.



