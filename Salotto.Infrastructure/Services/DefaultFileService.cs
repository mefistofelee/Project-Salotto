///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using System;
using System.IO;
using System.Text;

namespace Salotto.Infrastructure.Services
{
    /// <summary>
    /// Helper class to abstract loading of text files
    /// </summary>
    public class DefaultFileService : IFileService
    {
        private readonly string _root;

        public DefaultFileService(string root = "")
        {
            _root = root;
        }

        /// <summary>
        /// Load a (text) file optionally removing lines beginning with /, *, #
        /// </summary>
        /// <param name="path"></param>
        /// <param name="iso"></param>
        /// <param name="removeComments"></param>
        /// <returns></returns>
        public string Load(string path, string iso = null, bool removeComments = true)
        {
            var localizedPath = TemplateHelper.LocalizedFileName(path, iso);
            var file = Path.Combine(_root, localizedPath);
            if (!File.Exists(file))
            {
                // Try neutral format
                var neutralFile = Path.Combine(_root, path);
                if (!File.Exists(neutralFile))
                    return null;
                file = neutralFile;
            }

            var reader = new StreamReader(file);
            var text = reader.ReadToEnd();
            if (removeComments)
            {
                var builder = new StringBuilder();
                var lines = text.Split('\n');
                foreach (var line in lines)
                {
                    var temp = line.Trim();
                    if (temp.StartsWith("/") || temp.StartsWith("*") || temp.StartsWith("#"))
                        continue;
                    builder.AppendLine(temp);
                }

                text = builder.ToString();
            }
            
            reader.Close();
            return text;
        }

        /// <summary>
        /// When implemented, it will save (text) content back to a file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public void Save(string path, string content)
        {
            throw new NotImplementedException();
        }
    }
}