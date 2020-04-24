using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class T01 : MonoBehaviour
{
   
    public string userInput;//用户输入
    public List<string> userKeywords;//用户输入关键字

    public string result;//输出结果

    private void Start()
    {
        spWord();
    }


    public void spWord()
    {
        bool canDO = splitwords();
        Debug.Log(canDO);
    }

    //切分字符串并返回值；
    bool splitwords()
    {
        string str = userInput;
        int length = 0;//字符总长度

        for(int i = 0; i < userKeywords.Count;i++)
        {
            int index = 0;//字符位置
            int count = 0;//关键字个数
            int sCount = 0;//空格个数
            //Debug.Log(str.IndexOf(userKeywords[i], index));
            while ((index = str.IndexOf(userKeywords[i], index)) != -1)  //判断是否有匹配的单词
            {
                count++;

                if(index > 0)
                {
                    str = str.Insert(index, " ");
                    sCount++;
                }
                index = index + userKeywords[i].Length;
            }
            if(count == 0)  //如果一个匹配的单词都没有，则返回False
            {
                Debug.Log(" 切分失败：找不到与" + userKeywords[i] + "相匹配的单词。");
                return false; 
            }
            length += count * userKeywords[i].Length + sCount;    //根据关键字个数*关键字长度+空格长度 计算应当输出的字符串长度
        }

        //如果应当输出长度与实际输出长度不匹配，则返回False,有多余单字。
        if(length < str.Length)
        {
            Debug.Log("切分失败：输入的字符串内用冗余的字母。");
            return false;
        }
        else
        {
            result = str;
            Debug.Log(str);
            return true;
        }
    }
}
