using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class multithreading : MonoBehaviour {

    public GameObject dropdown;
    public GameObject[] dis = new GameObject[4];
    public GameObject menu_page;
    public Image [] process = new  Image[8];
    public float [] count_time = new float[8];
    public float [] start_time = new float[8];


    private Thread _t1;
    private Thread _t2;
    private Thread _t3;
    private Thread _t4;
    private bool play;
    private bool playing;
    private Dropdown n_thread;
    private float processTime;
    private float speed;
    private float GlobalTime;
    private int index_serial;
    private int[] check = new int[2];
	void Start () {
        for (int i = 0; i < 4; i++)
        {
            dis[i].SetActive(true);
        }
        menu_page.SetActive(false);
        GlobalTime = 0;
        speed = 0.5f;
        index_serial = 0;
        processTime = 5.0f;
        play = false;
        playing = false;
        n_thread = dropdown.GetComponent<Dropdown>();
        for (int i = 0; i < 8; i++)
        {
            start_time[i] = 0.0f;
            count_time[i] = 0.0f;
            process[i].GetComponent<Image>().fillAmount = 0;
        }

    }
	
	void Update () {
        GlobalTime += Time.deltaTime;

        if (play == true)
        {
            check[0] = check[1] = 0;
            GlobalTime = 0;
            index_serial = 0;
            print("playing!!");
            play = false;
            playing = true;
            for (int i = 0; i < 8; i++)
            {
                start_time[i] = 0.0f;
                count_time[i] = 0.0f;
                process[i].GetComponent<Image>().fillAmount = 0;
            }

            n_thread = dropdown.GetComponent<Dropdown>();

            stop_thread();

            if (n_thread.value == 1)
            {
                _t1 = new Thread(() => _func1(4));
                _t2 = new Thread(() => _func2(5));
                _t3 = new Thread(() => _func3(6));
                _t4 = new Thread(() => _func4(7));
                _t1.Start();
                _t2.Start();
                _t3.Start();
                _t4.Start();
            }
            else if(n_thread.value == 0)
            {
                _t1 = new Thread(() => _func1(4));
                _t2 = new Thread(() => _func2(5));
                _t1.Start();
                _t2.Start();
            }    
        }

        if (playing == true)
        {
            if (n_thread.value == 0)
            {
                if (count_time[4] >= processTime && check[0] == 0)
                {
                    check[0] = 1;
                    start_time[6] = GlobalTime;
                    _t1 = new Thread(() => _func3(6));
                    _t1.Start();
                }
                if (count_time[5] >= processTime && check[1] == 0)
                {
                    check[1] = 1;
                    start_time[7] = GlobalTime;
                    _t2 = new Thread(() => _func4(7));
                    _t2.Start();
                }
            }

            if (index_serial < 4)
            {
                if (count_time[index_serial] <= processTime)
                {
                    count_time[index_serial] += Time.deltaTime * speed;
                }
                else
                {
                    index_serial += 1;
                }

            }

            for(int i = 0; i<8;i++)
            {
                process[i].GetComponent<Image>().fillAmount = (count_time[i] / 5.0f);
            }
        }

        
    }

    public void pressExit()
    {
        stop_thread();
        Application.LoadLevel("menu");
    }
    public void pressResume()
    {
        for (int i = 0; i < 4; i++)
        {
            dis[i].SetActive(true);
        }
        menu_page.SetActive(false);
    }
    public void pressPause()
    {
        for(int i = 0;i < 4;i++)
        {
            dis[i].SetActive(false);
        }
        menu_page.SetActive(true);
    }
    public void pressStart()
    {
        play = true;
    }

    void stop_thread()
    {
        if (_t1 != null)
            if ((_t1.ThreadState & ThreadState.Running) == ThreadState.Running)
                { _t1.Interrupt(); _t1.Abort(); }

        if (_t2 != null)
            if ((_t2.ThreadState & ThreadState.Running) == ThreadState.Running)
                { _t2.Interrupt(); _t2.Abort(); }

        if (_t3 != null)
            if ((_t3.ThreadState & ThreadState.Running) == ThreadState.Running)
                { _t3.Interrupt(); _t3.Abort(); }

        if (_t4 != null)
            if ((_t4.ThreadState & ThreadState.Running) == ThreadState.Running)
                { _t4.Interrupt(); _t4.Abort(); }
    }

    void _func1(int num)
    {   
        while(count_time[num] <= processTime)
        {
            count_time[num] = (GlobalTime - start_time[num]) * speed;
        }
        _t1.Abort();
    }
    void _func2(int num)
    {
        while (count_time[num] <= processTime)
        {
            count_time[num] = (GlobalTime - start_time[num]) * speed;
        }
        _t2.Abort();
    }
    void _func3(int num)
    {
        while (count_time[num] <= processTime)
        {
            count_time[num] = (GlobalTime - start_time[num]) * speed;
        }
        _t3.Abort();
    }
    void _func4(int num)
    {
        while (count_time[num] <= processTime)
        {
            count_time[num] = (GlobalTime - start_time[num]) * speed;
        }
        _t4.Abort();
    }

}
