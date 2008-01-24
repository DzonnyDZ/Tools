#if Nightly || Alpha || Beta || RC || Release
/*
 * Copyright (C) 2006 Chris Stefano
 *       cnjs@mweb.co.za
 */
namespace Tools.Generators {
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Marks class as cutom tool
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CustomToolAttribute:Attribute {

        /// <summary>Contains value of the <see cref="Name"/> property</summary>
        protected string _name;

        /// <summary>Contains vaklue of the <see cref="Description"/> property</summary>
        protected string _description;

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
#endif