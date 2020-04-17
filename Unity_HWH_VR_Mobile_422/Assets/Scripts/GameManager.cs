using UnityEngine;
using System.Collections;  //引用 系統.集合 API:延遲 - 微軟 API

public class GameManager : MonoBehaviour
{
    //定意欄位 (宣告變數)
    //修飾詞 類型 名稱;
    //public 公開:顯示在屬性面板上
    //GameObject 遊戲物件:儲存階層面板內的物件
    /// <summary>
    /// 燈光群組
    /// </summary>
    [Header("會動的花台")]
    public Transform chest;
    [Header("燈光群組")]
    public GameObject groupLight;
    [Header("喇叭")]
    public AudioSource aud;
    [Header("鐵板上滑動音效")]
    public AudioClip soundWoodMove;
    [Header("敲門聲")]
    public AudioClip soundKonck;
    [Header("開門音效")]
    public AudioClip soundOpen;
    [Header("門的動畫控制器")]
    public Animator aniDoor;

    private int countDoor;      //看到門的次數

    public int countChest;      //看到花台的次數

    /// <summary>
    /// 看到門
    /// </summary>
    /// <returns></returns>
    public void LookDoor()
    {
        countDoor++;            //遞增1


            //如果    看到門的次數  等於1
            if(countDoor == 1)
        {
            aud.PlayOneShot(soundKonck, 5);
        }
        else if (countDoor == 2)
        {
            aud.PlayOneShot(soundOpen, 4.5f);
            aniDoor.SetTrigger("character_nearby");

        }
    }
    


    //定義方法(Method):有特定內容的功能
    //修飾詞 傳回類型 方法名稱 () {敘述}
    //void 無傳回:使用此方法不會得到任何資料
    //IEnumerator 延遲傳回
    //協同程序:多線程式處理方式
    /// <summary>
    /// 燈光閃爍效果
    /// </summary>
    public IEnumerator LieghtEffect()
    {
        //燈光群組.啟動設定(隱藏)
        groupLight.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        groupLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        groupLight.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        groupLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        groupLight.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        groupLight.SetActive(true);
    }
    
    /// <summary>
    /// 開始移動花台
    /// </summary>
    public void StartMoveChest()
    {
        StartCoroutine(MoveChest());

    }

    //注視點或按鈕 都無法呼教協程
    /// <summary>
    /// 移動花台
    /// </summary>
    /// <returns></returns>
    public IEnumerator MoveChest()
    {
        //GetComponent<泛型>() 取得元件:可以取得物件在屬性面板上的所有元件
        //enable 元件啟動或停止:true 啟動 false 停止 
        chest.GetComponent<BoxCollider>().enabled = false;

        //喇吧.播放一次音效(音效,音量)
        aud.PlayOneShot(soundWoodMove,1.5f);
        //前:forward
        //右:right
        //上:up
        //for 迴圈(初始值，條件，跌代器 - 每次迴圈結果要執行的敘述)
        for (int x = 0; x < 30; x++)
        {
            chest.position += chest.forward*0.1f;        //花台.座標 遞減 花台.前方
            yield return new WaitForSeconds(0.01f);
        }

    }


    //事件:開始 - 播放時執行一次，初始化或遊戲開始需要的內容
    private void Start()
    {
        //呼叫自定義方法
        //LieghtEffect();                       //呼叫自定一方法:一般呼教方式
        StartCoroutine(LieghtEffect());         //呼教協程方式
    }

}
