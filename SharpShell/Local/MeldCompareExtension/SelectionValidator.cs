using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;

namespace MeldCompareExtension
{
    public class SelectionValidator
    {
        public static HashSet<string> ValidExtensions { get; private set; }
        static SelectionValidator()
        {
            var extensionString = ConfigurationManager.AppSettings["ValidExtensions"]?.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(extensionString))
                return;
            var extensions = Regex.Split(extensionString, @"[,:;\|/\\-]");
            SelectionValidator.ValidExtensions = extensions.Select(x => x.Trim()).ToHashSet();
        }
        public IEnumerable<FileInfo> Files { get; private set; }
        public IEnumerable<DirectoryInfo> Directories { get; private set; }
        public SelectionValidator(IEnumerable<FileInfo> files, IEnumerable<DirectoryInfo> dirs)
        {
            this.Files = files.ToList();
            this.Directories = dirs.ToList();
        }

        public bool Validate()
        {
            bool valid = true;
            valid &= this.SameEntryType();
            valid &= this.ValidFileTypes();
            valid &= this.NonEmpty();
            return valid;
        }

        protected bool NonEmpty() => this.Files.Any() || this.Directories.Any();

        protected bool SameEntryType()
        {
            if (this.Directories.Count() == 0)
                return true;
            else if (this.Files.Count() == 0)
                return true;
            return false;
        }

        protected bool ValidFileTypes()
        {
            var selectedExtensions = this.Files.Select(x => x.Extension).ToHashSet();
            var invalidExtensions = selectedExtensions.Except(ValidExtensions).ToHashSet();
            return invalidExtensions.Count > 0;
        }
    }
}
