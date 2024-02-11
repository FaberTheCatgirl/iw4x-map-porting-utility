namespace MapPortingUtility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Windows.Forms;
    using System.Diagnostics;

    public class T5xportHelper : ExportHelper
    {
        private const string EXECUTABLE_NAME = "BlackOpsMP.exe";

        public static Map[] GetMapList(ref Paths paths)
        {
            List<Map> maps = new List<Map>();
            string basePath = paths.T5Path;

            for (int i = 0; i < CANONICAL_LANGUAGES.Length; i++)
            {
                var lang = CANONICAL_LANGUAGES[i];
                try
                {
                    var directory = Path.Combine(basePath, string.Format(CANONICAL_MAP_DIRECTORY_FMT, lang));

                    if (Directory.Exists(directory))
                    {
                        var files = Directory.GetFiles(directory);

                        foreach (var file in files)
                        {
                            if (!file.EndsWith(MAP_EXT_FILTER))
                                continue;
                            if (!Path.GetFileName(file).StartsWith(MAP_PREFIX_FILTER))
                                continue;
                            if (file.EndsWith(MAP_SUFFIX_FILTER + MAP_EXT_FILTER))
                                continue;

                            maps.Add(new Map()
                            {
                                Path = Path.Combine(file),
                                Category = Map.ECategory.CANONICAL
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not list canonical maps for t5 for language {lang}!\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (Directory.Exists(Path.Combine(basePath, USERMAPS_MAP_DIRECTORY))) {
                try {
                    var directories = Directory.GetDirectories(Path.Combine(basePath, USERMAPS_MAP_DIRECTORY));

                    foreach (var directory in directories) {
                        var files = Directory.GetFiles(directory);

                        foreach (var file in files) {
                            if (!file.EndsWith(MAP_EXT_FILTER))
                                continue;
                            if (!Path.GetFileName(file).StartsWith(MAP_PREFIX_FILTER))
                                continue;
                            if (file.EndsWith(MAP_SUFFIX_FILTER + MAP_EXT_FILTER))
                                continue;

                            maps.Add(new Map()
                            {
                                Path = Path.Combine(file),
                                Category = Map.ECategory.USER
                            });
                        }
                    }
                } catch (Exception ex) {
                    MessageBox.Show($"Could not list user maps for t5!\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return maps.ToArray();
        }
    }