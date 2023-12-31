﻿namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The rt element.
    /// </summary>
    sealed class HtmlRtElement : HtmlElement
    {
        public HtmlRtElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Rt, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
