namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML menuitem element.
    /// </summary>
    sealed class HtmlMenuItemElement : HtmlElement, IHtmlMenuItemElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML menuitem element.
        /// </summary>
        public HtmlMenuItemElement(Document owner, String? prefix = null)
            : base(owner, TagNames.MenuItem, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assigned master command, if any.
        /// </summary>
        public IHtmlElement? Command
        {
            get
            {
                var id = this.GetOwnAttribute(AttributeNames.Command);

                if (id is { Length: > 0 })
                {
                    return Owner?.GetElementById(id) as IHtmlElement;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the type of command.
        /// </summary>
        public String? Type
        {
            get => this.GetOwnAttribute(AttributeNames.Type);
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        /// <summary>
        /// Gets or sets the user-visible label.
        /// </summary>
        public String? Label
        {
            get => this.GetOwnAttribute(AttributeNames.Label);
            set => this.SetOwnAttribute(AttributeNames.Label, value);
        }

        /// <summary>
        /// Gets or sets the icon for the command.
        /// </summary>
        public String? Icon
        {
            get => this.GetOwnAttribute(AttributeNames.Icon);
            set => this.SetOwnAttribute(AttributeNames.Icon, value);
        }

        /// <summary>
        /// Gets or sets if the menuitem element is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get => this.GetBoolAttribute(AttributeNames.Disabled);
            set => this.SetBoolAttribute(AttributeNames.Disabled, value);
        }

        /// <summary>
        /// Gets or sets if the menuitem element is checked or not.
        /// </summary>
        public Boolean IsChecked
        {
            get => this.GetBoolAttribute(AttributeNames.Checked);
            set => this.SetBoolAttribute(AttributeNames.Checked, value);
        }

        /// <summary>
        /// Gets or sets if the menuitem element is the default command.
        /// </summary>
        public Boolean IsDefault
        {
            get => this.GetBoolAttribute(AttributeNames.Default);
            set => this.SetBoolAttribute(AttributeNames.Default, value);
        }

        /// <summary>
        /// Gets or sets the name of group of commands to
        /// treat as a radio button group.
        /// </summary>
        public String? RadioGroup
        {
            get => this.GetOwnAttribute(AttributeNames.Radiogroup);
            set => this.SetOwnAttribute(AttributeNames.Radiogroup, value);
        }

        #endregion
    }
}
