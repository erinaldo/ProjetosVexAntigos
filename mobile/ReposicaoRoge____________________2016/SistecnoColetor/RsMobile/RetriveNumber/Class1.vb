Imports System.Runtime.InteropServices
Public Class Class1

    <DllImport("sms.dll")> _
  Private Shared Function SmsGetPhoneNumber(ByVal psmsaAddress As IntPtr) As IntPtr
    End Function

    Private Function RetornaNumero() As String

        Dim number As IntPtr = Marshal.AllocHGlobal(512)
        Dim result As IntPtr = IntPtr.Zero
        Dim phoneNumber As String = ""

        Try
            result = SmsGetPhoneNumber(number)
        Catch ex As Exception
            Marshal.FreeHGlobal(number)
            'MessageBox.Show(ex.Message)
            Return phoneNumber
        End Try

        If (result.ToInt32 <> 0) Then

            'MessageBox.Show("Out of luck")
            Marshal.FreeHGlobal(number)
            Return phoneNumber

        End If

        phoneNumber = Marshal.PtrToStringUni(IntPtr.op_Explicit(System.Runtime.InteropServices.Marshal.SizeOf(GetType(Int32)) + number.ToInt32))

        Return phoneNumber

        'MessageBox.Show(phoneNumber)
        ''Marshal.FreeHGlobal(number)
    End Function

End Class
