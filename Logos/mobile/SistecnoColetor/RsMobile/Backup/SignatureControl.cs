using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.IO;


namespace PocketSignature
{

 /****************************************************************************
     *<author>Gitansu Behera emailId:gbehera@cellexchange.com</author>
     *<version>1.0</version>
     *<since>2004</since>
     *<summary>
     * Title:        SignatureControl
     * Version:      1.0
     * Author:       Geetansu Behera
     * Description:   Collects and displays a signature. The signature is made 
     * up of a list of line segments. Each segment contains an array of points 
     * (x and y coordinates). 
     * Draws to a memory bitmap to prevent flickering.
     * Raises the SignatureUpdate event when a new segment is added to 
     * the signature.
     *         Last updated
     * Modification Log :
     * Date             Author      Comments
     *
     * Unpublished Confidential Information of CellExchange.  Do not disclose.
     * Copyright © 1999-2004 CellExchange.com, Inc.  All Rights Reserved.
     ***************************************************************************
     * </summary>
     *<description></description>
     */

  public class SignatureControl : Control
  {


    private string background = null;

    public string Background
    {
      get 
      {
        return background;
      }
      set
      { 
        background = value;
  
      }

    }




    // gdi objects
    Bitmap bmp;
    //String bmpFile="\\Program Files\\SignCaptureV2\\sign here.png";
    
    Graphics graphics;
    Pen pen = new Pen(Color.Black);

    // list of line segments
    ArrayList pVector = new ArrayList();
    
    Point lastPoint = new Point(0,0);
    
    // if drawing signature or not
    bool drawSign = false;

    // notify parent that line segment was updated
    public event EventHandler SignatureUpdate;

    public SignatureControl(){}

    protected override void OnPaint(PaintEventArgs e) 
    {
      // we draw on the memory bitmap on mousemove so there
      // is nothing else to draw at this time 
      CreateGdiObjects();
      e.Graphics.DrawImage(bmp, 0, 0);
    }

    protected override void OnPaintBackground(PaintEventArgs e) 
    {
      // don't pass to base since we paint everything, avoid flashing
    }

    protected override void OnMouseDown(MouseEventArgs e) 
    {
      base.OnMouseDown(e);

      // process if currently drawing signature
      if (!drawSign)
      {
        // start collecting points
        drawSign = true;
        
        // use current mouse click as the first point
        lastPoint.X = e.X;
        lastPoint.Y = e.Y;
      }
    }

    protected override void OnMouseUp(MouseEventArgs e) 
    {
      
      base.OnMouseUp(e);
      // process if drawing signature
      if (drawSign)
      {
        // stop collecting points
        drawSign = false;   
      }
                  
    }
  
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);      

      // process if drawing signature
      if (drawSign)
      {

        // draw the new segment on the memory bitmap
        graphics.DrawLine(pen, lastPoint.X, lastPoint.Y, e.X, e.Y);
        pVector.Add(lastPoint.X+" "+lastPoint.Y+" "+e.X+" "+e.Y);
        // update the current position
        lastPoint.X = e.X;
        lastPoint.Y = e.Y;

        // display the updated bitmap
        Invalidate();
      } 
    }

    /// <summary>
    /// Clear the signature.
    /// </summary>
    public void Clear()
    {
      pVector.Clear();
      InitMemoryBitmap();
      Invalidate();
    }

    /// <summary>
    /// Create any GDI objects required to draw signature.
    /// </summary>
    private void CreateGdiObjects()
    {
      // only create if don't have one or the size changed
      if (bmp == null || bmp.Width != this.Width || bmp.Height != this.Height)
      {
        // memory bitmap to draw on
        InitMemoryBitmap();
      }
    }
    
    /// <summary>
    /// Create a memory bitmap that is used to draw the signature.
    /// </summary>
    private void InitMemoryBitmap()
    {
      // load the background image
      if (this.Background==null) 
      {
        bmp = new Bitmap(this.Width,this.Height);
        graphics = Graphics.FromImage(bmp);
        graphics.Clear(Color.White);
      }
      else 
      {
        bmp = new Bitmap(this.Background);
        // get graphics object now to make drawing during mousemove faster
        graphics = Graphics.FromImage(bmp);
      }
    }
    
    /// <summary>
    /// Notify container that a line segment has been added.
    /// </summary>
    private void RaiseSignatureUpdateEvent()
    {
      if (this.SignatureUpdate != null)
        SignatureUpdate(this, EventArgs.Empty);
    }
    
    private void CreateFile(String fileName,String fileContent) 
    {
      if (File.Exists(fileName)) 
      {
        Console.WriteLine("{0} already exists.", fileName);
        //return;
      }
      StreamWriter sr = File.CreateText(fileName);
      sr.WriteLine (fileContent);
      //MessageBox.Show("File creation complete");
      sr.Close();
    }

    public void  StoreSigData(String fileName)
    {
      string sigData = "";
      for (int i = 0; i<pVector.Count ;i++) 
      {
        // This commented operation is used to convert decimal to hex and store
        //sigData = sigData + PadHex(int.Parse(xVector[i].ToString()))+" " + PadHex(int.Parse(yVector[i].ToString()))+ "\n";
        sigData = sigData + pVector[i].ToString()+ "\n";
      }
      CreateFile(fileName,sigData); 
                              
    }
    // this method PadHex can be used to convert int to hex format //  
    // not in use
    private string PadHex(int inNum ) 
    {
      uint uiDecimal = checked((uint)System.Convert.ToUInt32(inNum));
      string s = String.Format("{0:x2}", uiDecimal);
      return(s);
    }
        

  }
}
