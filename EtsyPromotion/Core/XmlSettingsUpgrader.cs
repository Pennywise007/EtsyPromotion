using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using OpenQA.Selenium;

namespace EtsyPromotion.General
{
    /// <summary> Default program settings struct </summary>
    public class ProgramSettings
    {
        [XmlAttribute] public int SettingsVersion = 1;
    }

    /// <summary>
    /// Helping class for upgrading program settings from previous version to the most recent
    /// </summary>
    public class XmlSettingsUpgrader : IDisposable
    {
        private readonly string _filePath;
        private XmlDocument _xmlDoc;

        public XmlSettingsUpgrader(string filePath)
        {
            _filePath = filePath;

            try
            {
                _xmlDoc = new XmlDocument();
                _xmlDoc.Load(_filePath);
            }
            catch (Exception exception)
            {
                _xmlDoc = null;
                Debug.Assert(false, exception.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                _xmlDoc?.Save(_filePath);
            }
            catch (XmlException exception)
            {
                Debug.Assert(false, exception.Message);
            }
        }

        public int? GetCurrentSettingsVersion()
        {
            try
            {
                var attribute = _xmlDoc?.FirstChild?.Attributes?["SettingsVersion"];
                if (attribute == null || string.IsNullOrEmpty(attribute.Value))
                    return null;

                return int.Parse(attribute.Value);
            }
            catch (Exception exception)
            {
                Debug.Assert(false, exception.Message);
            }

            return null;
        }

        public void RenameAllNodes(string oldNodesName, string newNodesName)
        {
            if (_xmlDoc == null)
                return;

            try
            {
                XmlNodeList oldNamedNodeList = _xmlDoc.SelectNodes($"//*[starts-with(name(), '{oldNodesName}')]");

                if (oldNamedNodeList == null || oldNamedNodeList.Count == 0)
                    return;

                foreach (XmlNode oldNode in oldNamedNodeList)
                {
                    RenameNode(oldNode, newNodesName);
                }
            }
            catch (Exception exception)
            {
                Debug.Assert(false, exception.Message);
            }
        }

        public void AddNewSeparatorNodeBetweenNodes(string mainParentNodeName, string childrenNodesName, string newSeparatorNodeName)
        {
            if (_xmlDoc == null)
                return;

            try
            {
                XmlNodeList mainParentNodeList = _xmlDoc.SelectNodes($"//*[starts-with(name(), '{mainParentNodeName}')]");
                if (mainParentNodeList == null)
                    return;

                foreach (XmlNode mainNode in mainParentNodeList)
                {
                    XmlNode newSeparatorNode = null;

                    for (var index = 0; index < mainNode.ChildNodes.Count;)
                    {
                        var oldChildNode = mainNode.ChildNodes[index];
                        if (oldChildNode.Name == childrenNodesName)
                        {
                            if (newSeparatorNode == null)
                            {
                                newSeparatorNode = _xmlDoc.CreateElement(newSeparatorNodeName);
                                mainNode.InsertBefore(newSeparatorNode, oldChildNode);
                            }

                            newSeparatorNode.AppendChild(oldChildNode.CloneNode(true));
                            mainNode.RemoveChild(oldChildNode);
                        }
                        else
                        {
                            ++index;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.Assert(false, exception.Message);
            }
        }

        public void AddAttributeToNodes(string nodesName, string attributeName, string attributeValue)
        {
            if (_xmlDoc == null)
                return;

            try
            {
                XmlNodeList namedNodeList = _xmlDoc.SelectNodes($"//*[starts-with(name(), '{nodesName}')]");

                if (namedNodeList == null || namedNodeList.Count == 0)
                    return;

                foreach (XmlNode node in namedNodeList)
                {
                    if (node.Attributes == null)
                        continue;

                    XmlAttribute newAttribute = _xmlDoc.CreateAttribute(attributeName);
                    newAttribute.Value = attributeValue;

                    node.Attributes.Append(newAttribute);
                }
            }
            catch (Exception exception)
            {
                Debug.Assert(false, exception.Message);
            }
        }

        private void RenameNode(XmlNode oldNode, string newNodesName)
        {
            try
            {
                if (oldNode.ParentNode == null)
                    throw new InvalidElementStateException("Почему-то нет родительской ноды!");

                XmlNode newRoot = _xmlDoc.CreateElement(newNodesName);

                CopyAllChildNodes(ref newRoot, oldNode);

                CopyAllAttributes(ref newRoot, oldNode);

                oldNode.ParentNode.ReplaceChild(newRoot, oldNode);
            }
            catch (Exception exception)
            {
                Debug.Assert(false, exception.Message);
            }
        }

        private static void CopyAllChildNodes(ref XmlNode to, XmlNode from)
        {
            foreach (XmlNode childNode in from.ChildNodes)
            {
                try
                {
                    to.AppendChild(childNode.CloneNode(true));
                }
                catch (Exception exception)
                {
                    Debug.Assert(false, exception.Message);
                }
            }
        }

        private static void CopyAllAttributes(ref XmlNode to, XmlNode from)
        {
            if (from.Attributes == null)
                return;

            Debug.Assert(to.Attributes != null, "Почему-то нет атрибутов у созданной ноды!");
            if (to.Attributes == null)
                return;

            while (from.Attributes.Count > 0)
            {
                try
                {
                    to.Attributes.Append(from.Attributes[0]);
                }
                catch (ArgumentException exception)
                {
                    Debug.Assert(false, exception.Message);
                }
            }
        }
    }
}
