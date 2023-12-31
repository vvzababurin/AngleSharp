﻿namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class NodeIteratorTests
    {
        [Test]
        public void NodeIteratorJavaScriptKitDivision()
        {
            var source = @"<div id=contentarea>
<p>Some <span>text</span></p>
<b>Bold text</b>
</div>";
            var doc = source.ToHtmlDocument();
            var rootnode = doc.GetElementById("contentarea");
            var iterator = doc.CreateNodeIterator(rootnode, FilterSettings.Element);

            Assert.AreEqual(rootnode, iterator.Root);
            Assert.IsTrue(iterator.IsBeforeReference);

            var results = new List<INode>();

            while (iterator.Next() != null)
            {
                results.Add(iterator.Reference);
            }

            Assert.IsFalse(iterator.IsBeforeReference);
            Assert.AreEqual(4, results.Count);
            Assert.IsInstanceOf<HtmlDivElement>(results[0]);
            Assert.IsInstanceOf<HtmlParagraphElement>(results[1]);
            Assert.IsInstanceOf<HtmlSpanElement>(results[2]);
            Assert.IsInstanceOf<HtmlBoldElement>(results[3]);

            do
            {
                results.Remove(iterator.Reference);
            }
            while (iterator.Previous() != null);

            Assert.IsTrue(iterator.IsBeforeReference);
        }

        [Test]
        public void NodeIteratorJavaScriptKitParagraph()
        {
            var source = @"<p id=essay>George<span> loves </span><b>JavaScript!</b></p>";
            var doc = source.ToHtmlDocument();
            Assert.IsNotNull(doc);

            var rootnode = doc.GetElementById("essay");
            Assert.IsNotNull(rootnode);

            var iterator = doc.CreateNodeIterator(rootnode, FilterSettings.Text);
            Assert.IsNotNull(iterator);
            Assert.AreEqual(rootnode, iterator.Root);
            Assert.IsTrue(iterator.IsBeforeReference);

            Assert.AreEqual("George", iterator.Next().TextContent);

            var paratext = iterator.Reference.TextContent;

            while (iterator.Next() != null)
            {
                paratext += iterator.Reference.TextContent;
            }

            Assert.AreEqual("George loves JavaScript!", paratext);
        }

        [Test]
        public void NodeIteratorJavaScriptKitList()
        {
            var source = @"<ul id=mylist>
<li class='item'>List 1</li>
<li class='item'>List 2</li>
<li>List 3</li>
</ul>";
            var doc = source.ToHtmlDocument();
            Assert.IsNotNull(doc);

            var rootnode = doc.GetElementById("mylist");
            Assert.IsNotNull(rootnode);

            var iterator = doc.CreateNodeIterator(rootnode, FilterSettings.Element, node =>
            {

                if (node is IHtmlListItemElement element && element.ClassList.Contains("item"))
                {
                    return FilterResult.Accept;
                }

                return FilterResult.Reject;
            });

            Assert.IsNotNull(iterator);
            Assert.AreEqual(rootnode, iterator.Root);

            var results = new List<INode>();

            while (iterator.Next() != null)
            {
                results.Add(iterator.Reference);
            }

            Assert.AreEqual(7, rootnode.ChildNodes.Length);
            Assert.AreEqual(3, rootnode.Children.Length);
            Assert.AreEqual(2, results.Count);

            var item1 = results[0] as IHtmlListItemElement;
            var item2 = results[1] as IHtmlListItemElement;

            Assert.IsNotNull(item1);
            Assert.IsNotNull(item2);

            Assert.AreEqual("item", item1.ClassName);
            Assert.AreEqual("item", item2.ClassName);
        }

        [Test]
        public void NodeIteratorDotteroSpans()
        {
            var source = @"<div id=""content"">
        <span>
            <b>1. Section</b><br />
            <span>
                <b>1.1. Subsection</b><br />
            </span>
        </span>
        <span>
            <b>2.Section</b><br />
        </span>
    </div>";
            var doc = source.ToHtmlDocument();
            Assert.IsNotNull(doc);

            var rootnode = doc.GetElementById("content");
            Assert.IsNotNull(rootnode);

            var iterator = doc.CreateNodeIterator(rootnode, FilterSettings.Element,
                m => m.GetTagName() == "span" ? FilterResult.Accept : FilterResult.Skip);
            Assert.IsNotNull(iterator);
            Assert.AreEqual(rootnode, iterator.Root);

            var node = iterator.Next();
            var sections = 0;
            Assert.IsNotNull(node);

            while (node != null)
            {
                Assert.AreEqual("span", node.GetTagName());
                sections++;
                node = iterator.Next();
            }

            Assert.AreEqual(3, sections);
        }

        [Test]
        public void NodeIteratorFromDocumentDoesNotThrowException()
        {
            var doc = "<div></div>".ToHtmlDocument();
            var ni = doc.CreateNodeIterator(doc, FilterSettings.All);
            Assert.AreEqual(doc, ni.Root);
            Assert.AreEqual(doc, ni.Next());
            Assert.AreEqual(doc.DocumentElement, ni.Next());
            Assert.AreEqual(doc.Head, ni.Next());
            Assert.AreEqual(doc.Body, ni.Next());
            Assert.AreEqual(doc.Body.FirstChild, ni.Next());
            Assert.AreEqual(null, ni.Next());
        }

        [Test]
        public void NodeIteratorFromEmptyElementDoesNotThrowException()
        {
            var doc = "<div></div>".ToHtmlDocument();
            var div = doc.QuerySelector("div");
            var ni = doc.CreateNodeIterator(div, FilterSettings.All);
            Assert.AreEqual(div, ni.Root);
            Assert.AreEqual(div, ni.Next());
            Assert.AreEqual(null, ni.Next());
            Assert.AreEqual(div, ni.Previous());
            Assert.AreEqual(null, ni.Previous());
        }

        [Test]
        public void NodeIteratorUsingPreviousWorksAsExpected()
        {
            var doc = "<div><span>foo</span></div>".ToHtmlDocument();
            var div = doc.QuerySelector("div");
            var ni = doc.CreateNodeIterator(div, FilterSettings.Element);
            Assert.AreEqual(div, ni.Root);
            Assert.AreEqual(div, ni.Next());
            Assert.AreNotEqual(null, ni.Next());
            Assert.AreEqual(null, ni.Next());
            Assert.AreNotEqual(null, ni.Previous());
            Assert.AreEqual(div, ni.Previous());
            Assert.AreEqual(null, ni.Previous());
            Assert.AreEqual(div, ni.Next());
            Assert.AreEqual(div, ni.Previous());
            Assert.AreEqual(null, ni.Previous());
        }

        [Test]
        public void NodeIteratorUsingCommentsWithNoCommentsOnlyYieldsNull()
        {
            var doc = "<div><span>foo</span></div>".ToHtmlDocument();
            var div = doc.QuerySelector("div");
            var ni = doc.CreateNodeIterator(div, FilterSettings.Comment);
            Assert.AreEqual(div, ni.Root);
            Assert.AreEqual(null, ni.Next());
            Assert.AreEqual(null, ni.Next());
            Assert.AreEqual(null, ni.Previous());
            Assert.AreEqual(null, ni.Previous());
            Assert.AreEqual(null, ni.Next());
            Assert.AreEqual(null, ni.Previous());
        }
    }
}
