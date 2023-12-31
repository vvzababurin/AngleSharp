﻿namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The rb HTML element.
    /// </summary>
    sealed class HtmlRbElement : HtmlElement
    {
        public HtmlRbElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Rb, prefix, NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
