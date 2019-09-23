using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System.Collections.Generic;
using System.Linq;

namespace MeldCompareExtension
{
    /// <summary>
    /// The CountLinesExtensions is an example shell context menu extension,
    /// implemented with SharpShell. It adds the command 'Count Lines' to text
    /// files.
    /// </summary>
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.ClassOfExtension, ".txt")]
    public class MeldCompareExtension : SharpContextMenu
    {
        /// <summary>
        /// Determines whether this instance can a shell context show menu, given the specified selected file list.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance should show a shell context menu for the specified file list; otherwise, <c>false</c>.
        /// </returns>
        protected override bool CanShowMenu()
        {
            var validator = new SelectionValidator(this.SelectedFiles, this.SelectedDirectories);
            return validator.Validate();
        }

        /// <summary>
        /// Creates the context menu. This can be a single menu item or a tree of them.
        /// </summary>
        /// <returns>
        /// The context menu for the shell context menu.
        /// </returns>
        protected override ContextMenuStrip CreateMenu()
        {
            //  Create the menu strip.
            var menu = new ContextMenuStrip { Text = "Compare with Meld..." };
            menu.Click += (sender, e) =>
            {
                MeldInvoker.Invoke(this.SelectedItemPaths);
            };
            return menu;
        }
    }
}