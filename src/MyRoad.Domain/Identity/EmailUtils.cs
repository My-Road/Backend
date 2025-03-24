namespace MyRoad.Domain.Identity;

public static class EmailUtils
{
    public static string GetRegistrationEmailBody(string email, string name, string password, string loginLink)
    {
        return $@"
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='UTF-8'>
        <title>مرحبًا بك في MyRoad!</title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                padding: 20px;
                text-align: center;
            }}
            .container {{
                background-color: #fff;
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                max-width: 500px;
                margin: auto;
            }}
            h2 {{
                color: #333;
            }}
            .info {{
                background-color: #e3f2fd;
                padding: 10px;
                border-radius: 5px;
                text-align: left;
                margin-top: 15px;
            }}
            .footer {{
                margin-top: 20px;
                font-size: 14px;
                color: #666;
            }}
            .button {{
                display: inline-block;
                padding: 10px 20px;
                margin-top: 15px;
                background-color: #007bff;
                color: white;
                text-decoration: none;
                border-radius: 5px;
            }}
            .button:hover {{
                background-color: #0056b3;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h2>🎉 مرحبًا بك في MyRoad!</h2>
            <p>مرحبًا <b>{name}</b>, شكرًا لانضمامك إلينا!</p>

            <div class='info'>
                <p><strong>📧 Email:</strong> {email}</p>
                <p><strong>🔑 Password:</strong> {password}</p>
            </div>

            <a href='{loginLink}' class='button'>تسجيل الدخول الآن</a>
        </div>
    </body>
    </html>";
    }

    public static string GetPasswordResetSuccessEmailBody(string email, string name)
    {
        return $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='UTF-8'>
            <title>تم تغيير كلمة السر - MyRoad</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    padding: 20px;
                    text-align: center;
                }}
                .container {{
                    background-color: #fff;
                    padding: 20px;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    max-width: 500px;
                    margin: auto;
                }}
                h2 {{
                    color: #333;
                }}
                .info {{
                    background-color: #d4edda;
                    padding: 10px;
                    border-radius: 5px;
                    text-align: left;
                    margin-top: 15px;
                }}
                .footer {{
                    margin-top: 20px;
                    font-size: 14px;
                    color: #666;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>✅ تم تغيير كلمة السر بنجاح</h2>
                <p>مرحبًا <b>{name}</b>, تم تغيير كلمة السر الخاصة بحسابك بنجاح.</p>
                <div class='info'>
                    <p><strong>📧 Email:</strong> {email}</p>
                </div>
                <p class='footer'>إذا لم تقم بتغيير كلمة السر، يرجى التواصل مع فريق الدعم على الفور.</p>
            </div>
        </body>
        </html>";
    }
}