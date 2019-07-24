using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid SenderIdentityId { get; set; }
        public HashSet<Guid> RecipientIdentityIds { get; set; }
        public HashSet<Guid> ReadByIdentityIds { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public string Subject { get; set; }
        public HashSet<MessageFlag> Flags { get; set; }
        public MessageKind Kind { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}