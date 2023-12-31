﻿namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The rp HTML element.
    /// </summary>
    sealed class HtmlRpElement : HtmlElement
    {
        public HtmlRpElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Rp, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
