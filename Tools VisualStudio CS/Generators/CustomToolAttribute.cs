
/*
 * Copyright (C) 2006 Chris Stefano
 *       cnjs@mweb.co.za
 */
namespace Tools.VisualStudioT.GeneratorsT {
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Marks class as cutom tool
    /// </summary>
    /// <version version="1.5.2">Moved from namespace Tools.GeneratorsT to Tools.VisualStudioT.GeneratorsT</version>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CustomToolAttribute:Attribute {
        /// <summary>Contains value of the <see cref="Name"/> property</summary>
        protected string _name;
        /// <summary>Contains vaklue of the <see cref="Description"/> property</summary>
        protected string _description;
        /// <summary>Contains value of the <see cref="GeneratesDesignTimeSource"/> property</summary>
        private bool generatesDesignTimeSource= true;
        /// <summary>Gets or sets value indicating if custom tool generates design time source</summary>
        /// <value>Default value is <c>true</c></value>
        public bool GeneratesDesignTimeSource {
            get { return generatesDesignTimeSource; }
            set { generatesDesignTimeSource = value; }
        }
        /// <summary>Contains value of the <see cref="GeneratesDesignTimeSharedSource"/> property</summary>
        private bool generatesDesignTimeSharedSource = false;
        /// <summary>Gets or sets value indicating if custom tool generates design time shared source</summary>
        /// <value>Default value is <c>false</c></value>
        public bool GeneratesDesignTimeSharedSource {
            get { return generatesDesignTimeSharedSource; }
            set { generatesDesignTimeSharedSource = value; }
            }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="name">Name of the custom tool</param>
        public CustomToolAttribute(string name) :
            this(name, "") {
        }

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="name">Name of the custom tool</param>
        /// <param name="description">Tool description</param>
        public CustomToolAttribute(string name, string description) {
            this._name = name;
            this._description = description;
        }

        /// <summary>
        /// Name of the custom tool
        /// </summary>
        public string Name {get {return this._name;}}

        /// <summary>
        /// Tool description
        /// </summary>
        public string Description {get {return this._description;}}

    }
}