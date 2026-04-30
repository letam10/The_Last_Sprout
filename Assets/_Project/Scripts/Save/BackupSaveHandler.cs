using System.IO;

namespace TheLastSprout.Save
{
    public class BackupSaveHandler
    {
        private const int MAX_BACKUPS = 3;

        public void RotateBackups(string baseFilePath)
        {
            for (int i = MAX_BACKUPS - 1; i >= 1; i--)
            {
                string current = $"{baseFilePath}.bak{i}";
                string next = $"{baseFilePath}.bak{i + 1}";
                
                if (File.Exists(current))
                {
                    if (File.Exists(next)) File.Delete(next);
                    File.Move(current, next);
                }
            }

            string primaryBak = $"{baseFilePath}.bak";
            string firstRotated = $"{baseFilePath}.bak1";

            if (File.Exists(primaryBak))
            {
                if (File.Exists(firstRotated)) File.Delete(firstRotated);
                File.Move(primaryBak, firstRotated);
            }
        }

        public string TryLoadNewestValidBackup(string baseFilePath)
        {
            string[] backupExtensions = { ".bak", ".bak1", ".bak2", ".bak3" };

            foreach (var ext in backupExtensions)
            {
                string path = baseFilePath + ext;
                if (File.Exists(path))
                {
                    // Additional check for validity could be added here
                    return File.ReadAllText(path);
                }
            }

            return null;
        }
    }
}
