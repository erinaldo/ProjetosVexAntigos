package br.com.sistecno;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

public class Thread1 extends Thread{
	private Handler handler;
	
	public Thread1(Handler handler) {
		this.handler = handler;
	}
	
	@Override
	public void run() {
		int i = 0;
		while(i < 10000){
			Log.d("var i", i+"");
			i++;
		}
		
		//Invocando o método que sinalizará o fim da Thread
		Message msg = new Message();
		handler.sendMessage(msg);
	}
}


