using System.Collections.Generic;
using System.Linq;
using AxGrid.Base;
using AxGrid.Path;
using Core.Interfaces;
using Extensions;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISlotItem : MonoBehaviourExt
    {
        [SerializeField] private Image _mainItemImage;
        [SerializeField] private GridLayoutGroup _scrollLayout;
        [SerializeField] private RectTransform _setItemUpPos;
        [SerializeField] private float _bottomYPos;
        [SerializeField] private ParticleSystem _setParticle;

        [Header("Animation Settings")]
        [SerializeField] private float _setEaseTime;
        [SerializeField] private float _setTo;
        [Space]
        [SerializeField] private float _startScrollEaseTime;
        [SerializeField] private float _startScrollTo;
        [Space]
        [SerializeField] private float _scrollDeltaF;
        [SerializeField] private float _loopScrollLinearTime;
        [SerializeField] private float _loopScrollLinearTo;

        private Vector2 _initItemPos;
        
        private RectTransform _scrollLayoutRect;
        private RectTransform _mainItemRect;

        private MovableItemsScroll _movableItemsScroll;

        [OnAwake]
        private void OnAwake()
        {
            _mainItemRect = _mainItemImage.GetComponent<RectTransform>();
            _movableItemsScroll = new MovableItemsScroll(_scrollLayout);
        }
        
        public void SetMainSlot(ISlotModel slotModel)
        {
            PlaySlotVFX();
            _mainItemImage.gameObject.SetActive(true);
            _scrollLayout.gameObject.SetActive(false);
            _mainItemImage.sprite = slotModel.Item.Icon;
            _mainItemRect.anchoredPosition = _setItemUpPos.anchoredPosition;

            Path = CPath.Create()
                .EasingBounceEaseOutIn(_setEaseTime, 0, _setTo, (f) =>
                {
                    _mainItemRect.anchoredPosition =
                        Vector3.Lerp(_mainItemRect.anchoredPosition, _initItemPos, f);
                });
        }

        public void Init(ItemsModel itemsModel)
        {
            Path = CPath.Create()
                .Action((() => SetRandomMainItem(itemsModel)))
                .Action(() => CreateScrollItems(itemsModel))
                .Action(SetScrollStartPosition);
        }

        public void ScrollStart()
        { 
            _mainItemImage.gameObject.SetActive(false);
            _scrollLayout.gameObject.SetActive(true);
            _movableItemsScroll.ResetPos();
            SwapMainIconToLast();

            Path = CPath.Create()
                .EasingCubicEaseIn(_startScrollEaseTime, 0, _startScrollTo, (f) =>
                {
                    if (Path.DeltaF > _scrollDeltaF)
                    {
                        ScrollLoop();
                        return;
                    }
                    
                    _movableItemsScroll.MoveScroll(f,_bottomYPos, false);
                });
        }



        private void ScrollLoop()
        {
            Path = CPath.CreateLoop()
                .EasingLinear(_loopScrollLinearTime, 0, _loopScrollLinearTo, (f) =>
                {
                    _movableItemsScroll.MoveScroll(f,_bottomYPos);
                });
        }
        
        private void SwapMainIconToLast()
        {
            var lastScrollElement = _movableItemsScroll.CreatedItems.Last();
            lastScrollElement.sprite = _mainItemImage.sprite;
        }
        
        private void SetRandomMainItem(ItemsModel itemModels)
        {
            _initItemPos = _mainItemRect.anchoredPosition;
            var randItem = itemModels.Items.GetRandomElement();
            _mainItemImage.sprite = randItem.Icon;
        }
        
        private void CreateScrollItems(ItemsModel itemModels) => _movableItemsScroll.CreateScrollItems(itemModels);

        private void SetScrollStartPosition() => _movableItemsScroll.SetScrollStartPosition();

        private void PlaySlotVFX()
        {
            _setParticle.Stop();
            _setParticle.Play();
        }
    }

    public class MovableItemsScroll
    {
        private GridLayoutGroup _gridLayout;
        private List<Image> _createdItems;
        private Vector2 _initScrollPos;
        private RectTransform _gridLayourRect;
        
        public List<Image> CreatedItems => _createdItems;
        public MovableItemsScroll(GridLayoutGroup gridLayout)
        {
            _createdItems = new List<Image>();
            _gridLayout = gridLayout;
            _gridLayourRect = _gridLayout.GetComponent<RectTransform>();
        }


        public void ResetPos() => _gridLayourRect.position = _initScrollPos;
        
        public void MoveScroll(float time, float posY, bool checkPosition = true)
        {
            if (checkPosition && _gridLayourRect.anchoredPosition.y >= posY)
                _gridLayourRect.position = _initScrollPos;

            _gridLayourRect.anchoredPosition =
                Vector3.Lerp(_gridLayourRect.anchoredPosition,
                    new Vector3(_gridLayourRect.anchoredPosition.x, posY), time);
        }

        
        
        public void CreateScrollItems(ItemsModel itemModels)
        {
            IList<IItemModel> shuffledList = itemModels.Items.Shuffle();
            foreach (var item in shuffledList)
            {
                GameObject itemObject = new GameObject($"Item {item.ItemName}");
                var itemImg = itemObject.AddComponent<Image>();
                itemImg.sprite = item.Icon;
                itemObject.transform.SetParent(_gridLayout.transform);
                _createdItems.Add(itemImg);
            }
        }

        public void SetScrollStartPosition()
        {
            var offset = _gridLayout.spacing.y + _gridLayout.padding.top;
            var lastElement = _createdItems.Last();
            var lastElementYpos = lastElement.GetComponent<RectTransform>().anchoredPosition.y;
            _gridLayout.gameObject.SetActive(false);
            _gridLayourRect.anchoredPosition = new Vector2(0, -(lastElementYpos + offset));
            _initScrollPos = _gridLayourRect.position;
        }
        
    }
}