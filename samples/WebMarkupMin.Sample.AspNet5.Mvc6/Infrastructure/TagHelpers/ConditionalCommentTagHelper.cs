﻿using System;

using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace WebMarkupMin.Sample.AspNet5.Mvc6.Infrastructure.TagHelpers
{
	[HtmlTargetElement("conditional-comment")]
	public class ConditionalCommentTagHelper : TagHelper
	{
		[HtmlAttributeName("type")]
		public ConditionalCommentType CommentType { get; set; }

		[HtmlAttributeName("expression")]
		public string Expression { get; set; }


		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = null;

			ConditionalCommentType type = CommentType;

			string ifCommentStartPart;
			string ifCommentEndPart;

			switch (type)
			{
				case ConditionalCommentType.Hidden:
					ifCommentStartPart = "<!--[if ";
					ifCommentEndPart = "]>";

					break;
				case ConditionalCommentType.RevealedValidating:
					ifCommentStartPart = "<!--[if ";
					ifCommentEndPart = "]><!-->";

					break;
				case ConditionalCommentType.RevealedValidatingSimplified:
					ifCommentStartPart = "<!--[if ";
					ifCommentEndPart = "]>-->";

					break;
				case ConditionalCommentType.Revealed:
					ifCommentStartPart = "<![if ";
					ifCommentEndPart = "]>";

					break;
				default:
					throw new NotSupportedException();
			}

			TagHelperContent preContent = output.PreContent;
			preContent.Append(ifCommentStartPart);
			preContent.Append(Expression);
			preContent.Append(ifCommentEndPart);

			string endIfComment;

			switch (type)
			{
				case ConditionalCommentType.Hidden:
					endIfComment = "<![endif]-->";
					break;
				case ConditionalCommentType.RevealedValidating:
				case ConditionalCommentType.RevealedValidatingSimplified:
					endIfComment = "<!--<![endif]-->";
					break;
				case ConditionalCommentType.Revealed:
					endIfComment = "<![endif]>";
					break;
				default:
					throw new NotSupportedException();
			}

			output.PostContent.Append(endIfComment);
		}
	}
}