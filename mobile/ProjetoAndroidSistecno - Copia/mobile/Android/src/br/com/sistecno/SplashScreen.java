package br.com.sistecno;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.MotionEvent;

public class SplashScreen extends Activity
{          

	ProgressDialog mDialog;
	protected boolean _active = true;
    protected int _splashTime = 10; // time to display the splash screen in ms

    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.splashscreen);

        // thread for displaying the SplashScreen
        Thread splashTread = new Thread() {

            @Override
            public void run() {
                try {
                	
                    int waited = 0;
                    while (_active && (waited < _splashTime)) {
                        sleep(100);
                        if (_active) {
                            waited += 100;
                        }
                    }
                } catch (InterruptedException e) {
                    // do nothing
                } finally {
                	Intent intencao = new Intent(SplashScreen.this, MainActivity.class);
                	
                    startActivity(intencao);
                    finish();
                    
                }
            }
        };
        splashTread.start();

    }

	 protected void onResume()
	 {
		 super.onResume();	
		 mDialog = ProgressDialog.show(this, "Bem Vindo", "Carregando", false, false);
	 }
	 
	 protected void onDestroy()
	 {
		 super.onDestroy()	;
		 mDialog.dismiss();
	 }
    
    @Override
    public boolean onTouchEvent(MotionEvent event) {
        if (event.getAction() == MotionEvent.ACTION_DOWN) {
            _active = false;
        }
        return true;
    }


}