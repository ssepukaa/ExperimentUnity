using UnityEngine;
using System.IO;
using UnityEditor;

public class ScriptsCollector : MonoBehaviour {
    [MenuItem("Tools/Collect All Scripts")]
    static void CollectAllScripts() {
        string assetsPath = Application.dataPath; // Получаем путь к папке Assets
        string[] scriptFiles = Directory.GetFiles(assetsPath, "*.cs", SearchOption.AllDirectories); // Ищем все файлы .cs в папке Assets и подпапках
        string outputFile = Path.Combine(assetsPath, "AllScripts.txt"); // Файл для сохранения объединённого скрипта

        using (StreamWriter writer = new StreamWriter(outputFile, false)) // Создаём поток для записи в файл
        {
            foreach (string scriptFile in scriptFiles) {
                string contents = File.ReadAllText(scriptFile); // Читаем содержимое файла
                writer.WriteLine("// File: " + scriptFile); // Добавляем комментарий с именем файла
                writer.WriteLine(contents); // Записываем содержимое файла
                writer.WriteLine("\n\n"); // Добавляем разделители между файлами
            }
        }

        Debug.Log("All scripts have been collected to " + outputFile); // Выводим сообщение об успешной операции
    }
}