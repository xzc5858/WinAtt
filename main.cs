﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAtt
{
    public partial class main : Form
    {
        
        public main()
        {
            InitializeComponent();
            try
            {
                OpenForm("Outline");  //默认打开
                tv.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            OpenForm(e.Node.Name.Substring(2));
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="className"></param>
        private void OpenForm(string className)
        {
           

            Type classType = Type.GetType("WinAtt.frm" + className);              //得到类型

            if (classType != null) 
            {

                //先关闭所有子窗体
                Form[] arrForm = this.MdiChildren;
                for (int i = 0; i < arrForm.Length; i++)
                {
                    arrForm[i].Close();
                }


                Form childForm = (Form)System.Activator.CreateInstance(classType);          //创建此子窗体的一个新实例。

                childForm.MdiParent = this;                                                 //在显示该窗体前使其成为此 MDI 窗体的子窗体。
                childForm.Show();
                childForm.WindowState = FormWindowState.Maximized;                          //窗口最大化

            }
            else
            {


            }
        }

    }
}
