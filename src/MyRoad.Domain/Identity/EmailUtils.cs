namespace MyRoad.Domain.Identity;

public static class EmailUtils
{
    public static string GetRegistrationEmailBody(string email, string name, string password, string loginLink)
    {
        return $@"
            <!DOCTYPE html>
            <html lang='ar' dir='rtl'>
            <head>
                <meta charset='UTF-8'>
                <title>مرحبًا بك في MyRoad!</title>
            </head>
            <body style='font-family: Tahoma, Arial, sans-serif; background-color: #f0f2f5; margin: 0; padding: 20px; text-align: center;'>
                <div style='background-color: #ffffff; max-width: 500px; margin: auto; padding: 30px 20px; border: 1px solid #ddd; border-radius: 8px;'>
                    <h2 style='color: #2c3e50; font-size: 22px;'>🎉 مرحبًا بك في <b>MyRoad!</b></h2>
                    <p style='color: #333; font-size: 16px;'>مرحبًا <b>{name}</b></p>
                    <p style='color: #333; font-size: 16px;'>شكرًا لانضمامك إلينا! </p>
                
                    <div style='background-color: #e3f2fd; padding: 10px; text-align: right; direction: rtl; border-radius: 5px;'>
                        <p style='margin: 5px 0; font-size: 15px;'><b>📧 البريد الإلكتروني:</b> {email}</p>
                        <p style='margin: 5px 0; font-size: 15px;'><b>🔑 كلمة المرور:</b> {System.Net.WebUtility.HtmlEncode(password)}</p>
                    </div>

                    <a href='{loginLink}' style='display: inline-block; background-color: #007bff; color: white; text-decoration: none; padding: 12px 24px; border-radius: 6px; font-size: 16px; margin-top: 20px;'>تسجيل الدخول الآن</a>


                </div>
            </body>
            </html>";
    }


    public static string GetPasswordChangeSuccessEmailBody(string email, string name)
    {
        return $@"
            <!DOCTYPE html>
            <html lang='ar' dir='rtl'>
            <head>
                <meta charset='UTF-8'>
                <title>تم تغيير كلمة السر - MyRoad</title>
            </head>
            <body style='font-family: Tahoma, Arial, sans-serif; background-color: #f0f2f5; margin: 0; padding: 20px; text-align: center;'>
                <div style='background-color: #ffffff; max-width: 500px; margin: auto; padding: 30px 20px; border: 1px solid #ddd; border-radius: 8px;'>
                    <h2 style='color: #2c3e50; font-size: 22px;'>✅ تم تغيير كلمة السر بنجاح</h2>
                    <p style='color: #333; font-size: 16px;'>مرحبًا <strong>{name}</strong>.</p>
                    <p style='color: #333; font-size: 16px;'>تم تغيير كلمة السر الخاصة بحسابك بنجاح </p>

                    <div style='background-color: #d4edda; padding: 15px; text-align: right; direction: rtl; border-radius: 5px; margin-top: 20px;'>
                        <p style='margin: 8px 0; font-size: 15px;'><strong>📧 البريد الإلكتروني:</strong> {email}</p>
                    </div>

                    <p style='font-size: 13px; color: #666; margin-top: 20px;'>إذا لم تقم بتغيير كلمة السر، يرجى التواصل مع فريق الدعم على الفور.</p>
                </div>
            </body>
            </html>";
    }


    public static string GetPasswordResetEmailBody(string email, string name, string token, string userId)
    {
        string resetLink = $"https://frontend-emuj.onrender.com/reset-password?token={token}&userId={userId}";

        return $@"
            <!DOCTYPE html>
            <html lang='ar' dir='rtl'>
            <head>
                <meta charset='UTF-8'>
                <title>إعادة تعيين كلمة السر - MyRoad</title>
            </head>
            <body style='font-family: Tahoma, Arial, sans-serif; background-color: #f0f2f5; margin: 0; padding: 20px; text-align: center;'>
                <div style='background-color: #ffffff; max-width: 500px; margin: auto; padding: 30px 20px; border: 1px solid #ddd; border-radius: 8px;'>
                    <h2 style='color: #2c3e50; font-size: 22px;'>🔑 طلب إعادة تعيين كلمة السر</h2>
                    <p style='color: #333; font-size: 16px;'>مرحبًا <strong>{name}</strong>.</p>
                    <p style='color: #333; font-size: 16px;'>لقد طلبت إعادة تعيين كلمة السر لحسابك </p>
                    <p style='color: #333; font-size: 16px;'>إذا كنت قد قمت بهذا الطلب، يرجى الضغط على الزر أدناه لإعادة تعيين كلمة السر الخاصة بك:</p>

                    <a href='{resetLink}' style='display: inline-block; background-color: #007bff; color: white; padding: 12px 24px; border-radius: 6px; text-decoration: none; font-weight: bold; font-size: 16px; margin-top: 20px;'>إعادة تعيين كلمة السر</a>

                    <div style='background-color: #fff3cd; padding: 15px; text-align: right; direction: rtl; border-radius: 5px; margin-top: 20px;'>
                        <p style='margin: 8px 0; font-size: 15px;'><strong>📧 البريد الإلكتروني:</strong> {email}</p>
                    </div>

                    <p style='font-size: 13px; color: #888; margin-top: 25px;'>إذا لم تقم بطلب إعادة تعيين كلمة السر، يرجى تجاهل هذا البريد.</p>
                </div>
            </body>
            </html>";
    }


    public static string GetPasswordChangedEmailBody(string fullName)
    {
        return $@"
            <!DOCTYPE html>
            <html lang='ar' dir='rtl'>
            <head>
                <meta charset='UTF-8'>
                <title>تم تغيير كلمة السر - MyRoad</title>
            </head>
            <body style='font-family: Tahoma, Arial, sans-serif; background-color: #f0f2f5; margin: 0; padding: 20px; text-align: center;'>
                <div style='background-color: #ffffff; max-width: 500px; margin: auto; padding: 30px 20px; border: 1px solid #ddd; border-radius: 8px;'>
                    <h2 style='color: #2c3e50; font-size: 22px;'>✅ تم تغيير كلمة السر بنجاح</h2>
                    <p style='color: #333; font-size: 16px;'>مرحبًا <strong>{fullName}</strong>.</p>
                    <p style='color: #333; font-size: 16px;'>تم تغيير كلمة السر الخاصة بحسابك بنجاح.</p>

                    <div style='background-color: #d4edda; padding: 15px; text-align: right; direction: rtl; border-radius: 5px; margin-top: 20px;'>
                        <p style='margin: 8px 0; font-size: 15px;'>لقد قمت بتغيير كلمة المرور الخاصة بك بنجاح. إذا لم تكن قد قمت بهذه العملية، يرجى التواصل مع فريق الدعم فورًا.</p>
                    </div>

                </div>
            </body>
            </html>";
    }
}