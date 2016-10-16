# UWP-Messaging-App
A messaging app made in UWP


##Setup

Using instructions here: http://linoxide.com/linux-how-to/install-couchdb-futon-ubuntu-1604/ 
I configured CouchDB on a Ubuntu 16.04 Server on Azure.

Azure VM DNS: uwp-couchdb.westeurope.cloudapp.azure.com

##Database Desgin

CouchDB is being used as the database.

####Users
userID,<br>
username,<br>
email,<br>
firstName,<br>
lastName,<br>
securityQ1,  // security questions for resetting password<br>
securityQ2<br>

####Paswords
passwordID,<br>
userID  // to link password to user<br>
password<br>

####Conversations
convoID,<br>
userOneID,  // ids of the two users engaging in conversation<br>
userTwoID<br>

####ConvoMessages
convoMessagesID,<br>
convoID,  // the id of the conversation, links the users involved in conv<br>
Messages[] // list of messages in convo, of type Message (Seen Below)<br>

####Message Type
// structure of the actual message object being stored in convoMessages<br>
messageID,<br>
userID,   // the id of the user who send message<br>
messageBody  // actual message text<br>
