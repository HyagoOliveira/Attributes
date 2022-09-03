using System;
using UnityEngine;

namespace ActionCode.Attributes
{
    /// <summary>
    /// Adds a Create Button if no reference is currently set.
    /// <para>Use it on <see cref="ScriptableObject"/> attributes.</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class CreateButtonAttribute : PropertyAttribute
    {
        /// <summary>
        /// The type of the asset to create. It must inherits from <see cref="ScriptableObject"/>.
        /// </summary>
        public readonly Type Type;

        /// <summary>
        /// The path to open the Save Panel Window.
        /// </summary>
        public readonly string Path;

        /// <summary>
        /// Adds a Create Button if no reference is currently set.
        /// </summary>
        /// <param name="type">The type of the asset to create. It must inherits from <see cref="ScriptableObject"/>.</param>
        /// <param name="path">The path to open the Save Panel Window.</param>
        public CreateButtonAttribute(Type type, string path = "Assets/")
        {
            Path = path;
            Type = type;
        }
    }
}