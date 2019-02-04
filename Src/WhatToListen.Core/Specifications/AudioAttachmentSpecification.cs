using WhatToListen.Core.Entities.Vk.Attachments;

namespace WhatToListen.Core.Specifications
{
	public class AttachmentEmptySpecification : BaseSpecification<Attachment>
	{
		public AttachmentEmptySpecification(string type, int skip = 0, int take = 100)
			: base(i => i.Type == type)
		{
			ApplyPaging(skip, take);
		}
	}
}
