using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Messaging_App.Data
{
    // service for getting conversations from couchDB
    class ConversationService
    {
        // the conversation by its id
        private List<Conversation> conversations;

        public Conversation getConversationByID(string id)
        {
            // get the conversation from couchDB

            Conversation c;
            Message m;

            c = new Conversation();
            c.id = "c1";
            c.userIds = new List<string>();
            c.userIds.Add("u1");
            c.userIds.Add("u2");
            c.messages = new ObservableCollection<Message>();

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u1";
            m.message = "kjenrgknerkgjnerkjgnekjrgnkejrngkjerngkjerngkjn jne jn kej nekjr kej nekj nekj nek jnerk jnek jnek je kjne kjn kjer kjnerkj nerkj nekrjn ekrj nekrj nekrj 5";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u1";
            m.message = "kjenrgknerkgjnerkjgnekjrgnkejrngkjerngkjerngkjn jne jn kej nekjr kej nekj nekj nek jnerk jnek jnek je kjne kjn kjer kjnerkj nerkj nekrjn ekrj nekrj nekrj 5";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u1";
            m.message = "kjenrgknerkgjnerkjgnekjrgnkejrngkjerngkjerngkjn jne jn kej nekjr kej nekj nekj nek jnerk jnek jnek je kjne kjn kjer kjnerkj nerkj nekrjn ekrj nekrj nekrj 5";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u1";
            m.message = "kjenrgknerkgjnerkjgnekjrgnkejrngkjerngkjerngkjn jne jn kej nekjr kej nekj nekj nek jnerk jnek jnek je kjne kjn kjer kjnerkj nerkj nekrjn ekrj nekrj nekrj 5";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u1";
            m.message = "kjenrgknerkgjnerkjgnekjrgnkejrngkjerngkjerngkjn jne jn kej nekjr kej nekj nekj nek jnerk jnek jnek je kjne kjn kjer kjnerkj nerkj nekrjn ekrj nekrj nekrj 5";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u1";
            m.message = "kjenrgknerkgjnerkjgnekjrgnkejrngkjerngkjerngkjn jne jn kej nekjr kej nekj nekj nek jnerk jnek jnek je kjne kjn kjer kjnerkj nerkj nekrjn ekrj nekrj nekrj 5";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u1";
            m.message = "kjenrgknerkgjnerkjgnekjrgnkejrngkjerngkjerngkjn jne jn kej nekjr kej nekj nekj nek jnerk jnek jnek je kjne kjn kjer kjnerkj nerkj nekrjn ekrj nekrj nekrj 5";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u2";
            m.message = "Hey there";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.id = Guid.NewGuid().ToString();
            m.message = "Blah blah blah";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u2";
            m.message = "Damn I hate you";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u1";
            m.message = "Thanks, you too.";
            c.messages.Add(m);

            m = new Message();
            m.id = Guid.NewGuid().ToString();
            m.senderId = "u2";
            m.message = "<3";
            c.messages.Add(m);

            return c;

        } // getConversationByID()

        // gets all of the conversations for a user
        public List<Conversation> getUsersCOnversations(string userId)
        {
            // get the conversation from couchDB
            // use the getConversationByID method

            return null;

        } // getUsersCOnversations()

    } // class

} // namespace
