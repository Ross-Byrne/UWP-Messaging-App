# UWP-Messaging-App
A messaging app made in UWP

##Introduction
This messaging app allows users to sign up with a username and password. They can then login, add other users who use the app and message them.

##Setup

Using instructions here: http://linoxide.com/linux-how-to/install-couchdb-futon-ubuntu-1604/ 
I configured CouchDB on a Ubuntu 16.04 Server on Azure.

Azure VM DNS: uwp-couchdb.westeurope.cloudapp.azure.com

##Database Desgin

CouchDB is being used as the database.

CouchDB has the following databases:

###_users
Contains the users for the messaging app. These users are also managed by couchDB's authentication system.

###contacts
Cotains the contact object. It is created when two users add eachother as contacts.

###conversations
Contains the conversation object. There is one object for each conversation between users.

###messages
Contains the message objects

##Security
All calls to couchDB, other then creating the user, are authenticated with the logged in users login details.

##References
####Loading Gif
http://ajaxload.info/
