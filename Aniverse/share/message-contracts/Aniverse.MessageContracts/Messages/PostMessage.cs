﻿namespace Aniverse.MessageContracts.Messages
{
    public class PostMessage
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        public IList<string> FilesName { get; set; }
        public string Content { get; set; }
    }
}
