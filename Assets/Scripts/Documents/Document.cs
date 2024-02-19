using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.MVC.Documents
{
    //[ExecuteInEditMode]
    public class Document : MonoBehaviour
    {
        [SerializeField] private List<DocumentPaper> _documentPapers;
        [SerializeField] private DocumentPaper _documentPaperPrefab;
        [SerializeField] private Transform _documentPaperParent;
        [SerializeField] private string _spritesFolderPath;

        //private void OnEnable()
        //{
        //    //CreatePapers();
        //}

        public void CreatePapers()
        {       
            Sprite[] sprites = Resources.LoadAll<Sprite>(_spritesFolderPath);

            // Проходимся по каждому спрайту
            foreach (Sprite sprite in sprites)
            {
                DocumentPaper documentPaper = Instantiate(_documentPaperPrefab, _documentPaperParent);
                documentPaper.SetPaper(sprite);
                _documentPapers.Add(documentPaper);
            }
            //foreach (var paper in _documentPapers)
            //    paper.gameObject.transform.SetParent(null);
            //List<Sprite> sprites = LoadSprites();
            //foreach (Sprite sprite in sprites)
            //{
            //    DocumentPaper documentPaper = Instantiate(_documentPaperPrefab, _documentPaperParent);
            //    documentPaper.SetPaper(sprite);
            //    _documentPapers.Add(documentPaper);
            //}
        }

        private List<Sprite> LoadSprites()
        {
            // Получаем путь к папке с спрайтами в ресурсах
            string path = Path.Combine(Application.dataPath, _spritesFolderPath);

            // Получаем список всех файлов в папке
            string[] files = Directory.GetFiles(path);

            // Создаем список для хранения спрайтов
            List<Sprite> sprites = new List<Sprite>();

            // Проходим по всем файлам
            foreach (string file in files)
            {
                // Загружаем текстуру
                Texture2D texture = LoadTexture(file);
                if (texture != null)
                {
                    // Создаем спрайт из текстуры
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    sprites.Add(sprite);
                }
            }

            return sprites;
        }

        private Texture2D LoadTexture(string filePath)
        {
            // Загружаем бинарные данные из файла
            byte[] fileData = File.ReadAllBytes(filePath);
            if (fileData == null)
            {
                Debug.LogError("Failed to load file at path: " + filePath);
                return null;
            }

            // Создаем новую текстуру
            Texture2D texture = new Texture2D(2, 2);
            if (!texture.LoadImage(fileData))
            {
                Debug.LogError("Failed to load image from file: " + filePath);
                return null;
            }

            return texture;
        }
    }
}