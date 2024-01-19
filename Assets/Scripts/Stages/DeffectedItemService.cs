using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Stages
{
    public class DeffectedItemService : MonoBehaviour
    {
        [SerializeField] private bool _isCanPierced;
        [SerializeField] private GameObject[] _deffects;
        [SerializeField] private Texture _dirtyTextures;
        [SerializeField] private MeshRenderer[] _tables;
        [SerializeField] private Texture _baseTableTexutre;

        public bool IsDeffected { get; private set; }
        public bool IsDeffectedGloves { get; private set; }

        private MeshRenderer[] _meshRender;
        private SkinnedMeshRenderer[] _skinMeshRender;
        private Texture _baseTexutre;


        private void Awake()
        {
            _meshRender = GetComponentsInChildren<MeshRenderer>();
            _meshRender = _meshRender.Where(item => item.gameObject.name != "Text (TMP)" && item.gameObject.name != "Табличка" && item.gameObject.name != "Табличка 1" && item.gameObject.name != "Табличка 2").ToArray();
            if (_meshRender != null)
                if (_meshRender.Length > 0)
                    _baseTexutre = _meshRender[0].materials[0].mainTexture;

            _skinMeshRender = GetComponentsInChildren<SkinnedMeshRenderer>();
            _skinMeshRender = _skinMeshRender.Where(item => item.gameObject.name != "Text (TMP)" && item.gameObject.name != "Табличка" && item.gameObject.name != "Табличка 1" && item.gameObject.name != "Табличка 2").ToArray();
            if (_skinMeshRender != null)
            {
                if (_skinMeshRender.Length > 0)
                    _baseTexutre = _skinMeshRender[0].materials[0].mainTexture;
            }
        }

        public void OffDeffects()
        {
            if (!IsDeffected)
                return;
            IsDeffectedGloves = false;
            if (_deffects != null)
            {
                if (_deffects.Length > 0)
                {
                    foreach (var item in _deffects)
                        item.SetActive(false);
                }
            }

            if (_meshRender != null)
            {
                if (_meshRender.Length > 0)
                {
                    foreach (var item in _meshRender)
                    {
                        item.materials[0].mainTexture = _baseTexutre;
                    }
                }
            }

            if (_skinMeshRender != null)
            {
                if (_skinMeshRender.Length > 0)
                {
                    foreach (var item in _skinMeshRender)
                    {
                        item.materials[0].mainTexture = _baseTexutre;
                        item.materials[1].mainTexture = _baseTexutre;
                    }
                }
            }
            if (_tables.Length > 0)
            {
                foreach (var item in _tables)
                {
                    item.material.mainTexture = _baseTableTexutre;
                }
            }
            IsDeffected = false;
        }

        public void DisplayDeffectsWithRandomValue()
        {
            Debug.Log("Start Deffect");

            int randomValue = Random.Range(0, 99);

            if(randomValue > 60)
            {

                if (_isCanPierced)
                {
                    int randomValue1 = Random.Range(0, 99);
                    if (randomValue1 >= 50)
                    {
                        IsDeffectedGloves = true;
                    }
                    else
                    {
                        if (_deffects.Length > 0)
                            TryDisplayDeffects();
                        if (_dirtyTextures != null)
                            TryDisplayDirtyTexture();
                    }

                }
                else
                {
                    if (_deffects.Length > 0)
                        TryDisplayDeffects();
                    if (_dirtyTextures != null)
                        TryDisplayDirtyTexture();
                }
            }

        }


        private void TryDisplayDeffects()
        {
            if (_deffects == null)
                return;
            if (_deffects.Length > 0)
            {
                foreach (var item in _deffects)
                    item.SetActive(true);
                IsDeffected = true;
            }
        }

        private void TryDisplayDirtyTexture()
        {
            TryPaintInDirtyTextureMeshRenderer();
            TryPaintInDirtyTextureSkinMeshRenderer();
        }

        private void TryPaintInDirtyTextureMeshRenderer()
        {
            if (_meshRender == null)
                return;
            if(_meshRender.Length > 0)
            {
                foreach(var item in _meshRender)
                {
                    item.materials[0].mainTexture = _dirtyTextures;
                }
                IsDeffected = true;
            }
        }

        private void TryPaintInDirtyTextureSkinMeshRenderer()
        {
            if (_skinMeshRender == null)
                return;
            if (_skinMeshRender.Length > 0)
            {
                foreach (var item in _skinMeshRender)
                {
                    item.materials[0].mainTexture = _dirtyTextures;
                    item.materials[1].mainTexture = _dirtyTextures;
                }
                IsDeffected = true;
            }
        }
    }
}