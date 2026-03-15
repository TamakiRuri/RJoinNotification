
using System;
using net.puk06.CanvasAnimation;
using net.puk06.CanvasAnimation.Models;
using net.puk06.CanvasAnimation.Utils;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace com.rurinya.joinnotification
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RJoinNotificationObject : UdonSharpBehaviour
    {
        

        [Header("スペースが文字数で変わらないため、文字数が多くなると表示がおかしくなる場合があります。")]
        [SerializeField] private string joinText = "Join";
        [SerializeField] private string exitText = "Exit";
        [Space(10)]
        [Header("アニメーションの長さなど")]
        [SerializeField] private float transitionInTime = 0.4f;
        [SerializeField] private float transitionOutTime = 0.4f;
        [SerializeField] private float stayTime = 1f;
        [SerializeField] private bool allowTextAnimation = true;
        [SerializeField] private float intervalPerChar = 0.1f;
        [Space(10)]
        [SerializeField] private Image background;
        [SerializeField] private Image statusBubble;
        // [SerializeField] private TMP_Text joinTextTMP;
        // [SerializeField] private TMP_Text exitTextTMP;
        // [SerializeField] private TMP_Text usernameTMP;

        [SerializeField] private Text joinTextTMP;
        [SerializeField] private Text exitTextTMP;
        [SerializeField] private Text usernameTMP;
        
        // [SerializeField] private float backgroundDefaultLength;
        // [SerializeField] private float statusBubbleDefaultLength;
        // [SerializeField] private float usernameDefaultLength;
        // [SerializeField] private float paddingLength;
        [SerializeField] private Color joinInfoColor;
        [SerializeField] private Color exitInfoColor;
        [SerializeField] private CanvasAnimationSystem canvasAnimationSystem;

        private Component[] animatedComponents;
        private Vector3 defaultScale;
        private Vector3 targetScale;

        // Text Animation

        // private float timeStamp;
        // private string tempUsername;
        // private int animIndex;

        public void SetInfo(bool status, string username, bool hasBackground, int mode)
        {
            //tempUsername = username;
            background.gameObject.SetActive(hasBackground);
            if (status == true){
                // statusText.text="Join";
                joinTextTMP.gameObject.SetActive(true);
                exitTextTMP.gameObject.SetActive(false);

                statusBubble.color = joinInfoColor;
            }
            else {
                // statusText.text="Exit";
                joinTextTMP.gameObject.SetActive(false);
                exitTextTMP.gameObject.SetActive(true);

                statusBubble.color = exitInfoColor;
            }
            usernameTMP.text = username;
            AnimationController(mode);
            //float targetBackgroundLength = statusBubbleDefaultLength + usernameText.text.Length*10 + paddingLength;
            //Debug.Log(targetBackgroundLength);
            //background.GetComponent<RectTransform>().sizeDelta = new Vector2(targetBackgroundLength, stayTime00);
            //background.transform.position = new Vector3((targetBackgroundLength - backgroundDefaultLength)/2,background.transform.position.y,background.transform.position.z);
            //bubble.GetComponent<RectTransform>().sizeDelta = new Vector2(usernameDefaultLength + usernameText.text.Length, 90);
            //bubble.GetComponent<RectTransform>().sizeDelta = new Vector2(usernameDefaultLength + bubble.gameObject.GetComponent<RectTransform>().sizeDelta.x * 0.5f, stayTime00);

            //gameObject.GetComponent<Animator>().SetBool("isActive", true);
        }
        void Start()
        {
            defaultScale = background.transform.localScale;
            canvasAnimationSystem
                .DefineTransform(animatedComponents, new Vector3(1,1,1), TransformType.Scale)
                .SaveTransform(animatedComponents, new TransformType[]{TransformType.Position});
                // .Hide(background)
                // .Hide(statusBubble)
                // .Hide(statusText)
                // .Hide(usernameText);
        }
        // void Update()
        // {
        //     if (allowTextAnimation)
        //     {
        //         if(Time.timeSinceLevelLoad > timeStamp + intervalPerChar)
        //         {
        //             if (animIndex <= tempUsername.Length)
        //             {
        //                 usernameTMP.text = tempUsername.Substring(0, animIndex);
        //                 timeStamp = Time.timeSinceLevelLoad;
        //             }
        //         }
        //     }
        // }
        private void Starter(int mode)
        {
            
            if (background!= null && statusBubble != null && joinTextTMP != null && exitTextTMP != null && usernameTMP != null)
            animatedComponents = new Component[] {background, statusBubble, joinTextTMP, exitTextTMP,  usernameTMP};
            else
            {
                Debug.LogError("RJoinNotification: アニメーションで使用されるコンポーネントが初期化されていません。");
                gameObject.SetActive(false);
                return;
            }
            if (mode != 1)
            canvasAnimationSystem
                .Cancel(animatedComponents)
                .SaveTransform(animatedComponents, new TransformType[]{TransformType.Position});
                // .Hide(background)
                // .Hide(statusBubble)
                // .Hide(statusText)
                // .Hide(usernameText);
            else
            canvasAnimationSystem
                .Cancel(animatedComponents);
        }
        private void AnimationController(int mode)
        {
            Starter(mode);
            // Not In Use
            // if(allowTextAnimation)
            // {
            //     timeStamp = Time.timeSinceLevelLoad;
            //     animIndex = 0;
            // }

            switch (mode)
            {
                case 0:
                    AnimFadeIn();
                    return;
                case 1:
                    AnimPop();
                    return;
                case 2:
                    AnimFadeInLeft();
                    return;
                case 3:
                    AnimFadeInRight();
                    return;
                case 4:
                    AnimFadeInDown();
                    return;
                default:
                    Debug.LogError("RJoinNotification: モードは0~4しか登録できません。");
                    return;
            }
        }


        // CANVAS ANIMATION SYSTEM

        private void AnimFadeIn()
        {
            canvasAnimationSystem
                // .Cancel(animatedComponents)
                .ResetTransform(animatedComponents, new TransformType[] {TransformType.Position})
                .Fade(background, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(statusBubble, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(joinTextTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(exitTextTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(usernameTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(background, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(statusBubble, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(joinTextTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(exitTextTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(usernameTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut);
        }
        private void AnimPop()
        {
            targetScale = new Vector3((float)1.2*defaultScale.x, (float)1.2*defaultScale.y,(float)1.2*defaultScale.z);
            canvasAnimationSystem
                // .Cancel(animatedComponents)
                .ResetTransform(animatedComponents, new TransformType[] {TransformType.Scale})
                .Scale(animatedComponents, transitionInTime, 0, AnimationDirection.From, new Vector3(0,0,0), TransitionType.Linear)
                //.Scale(animatedComponents, transitionInTime*0.2f, transitionInTime, AnimationDirection.To, targetScale, TransitionType.Linear)
                //.Scale(animatedComponents, 0.1f, 0, AnimationDirection.From, targetScale, TransitionType.EaseInOut)
                .Scale(animatedComponents, transitionOutTime, stayTime+transitionInTime, AnimationDirection.To, new Vector3(0,0,0), TransitionType.EaseInOut);
        }
        private void AnimFadeInLeft()
        {
            canvasAnimationSystem
                // .Cancel(animatedComponents)
                .ResetTransform(animatedComponents, new TransformType[] {TransformType.Position})
                .Fade(background, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(statusBubble, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(joinTextTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(exitTextTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(usernameTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Move(animatedComponents, transitionInTime, 0, 200, MoveDirection.Right, TransitionType.EaseInOut )
                .Fade(background, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(statusBubble, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(joinTextTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(exitTextTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(usernameTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut);
        }

        private void AnimFadeInRight()
        {
            canvasAnimationSystem
                // .Cancel(animatedComponents)
                .ResetTransform(animatedComponents, new TransformType[] {TransformType.Position})
                .Fade(background, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(statusBubble, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(joinTextTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(exitTextTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(usernameTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Move(animatedComponents, transitionInTime, 0, 200, MoveDirection.Left, TransitionType.EaseInOut )
                .Fade(background, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(statusBubble, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(joinTextTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(exitTextTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(usernameTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut);
        }

        private void AnimFadeInDown()
        {
            canvasAnimationSystem
                // .Cancel(animatedComponents)
                .ResetTransform(animatedComponents, new TransformType[] {TransformType.Position})
                .Fade(background, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(statusBubble, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(joinTextTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(exitTextTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Fade(usernameTMP, transitionInTime, 0, FadeType.In, TransitionType.EaseInOut)
                .Move(animatedComponents, transitionInTime, 0, 50, MoveDirection.Up, TransitionType.EaseInOut )
                .Fade(background, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(statusBubble, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(joinTextTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(exitTextTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut)
                .Fade(usernameTMP, transitionOutTime, stayTime+transitionInTime, FadeType.Out, TransitionType.EaseInOut);
        }
    }

}
