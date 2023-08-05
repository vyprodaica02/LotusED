using BaiThucTaplan1.InputHellper;
using BaiThucTaplan1.ISerVices;
using BaiThucTaplan1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

// Khởi tạo WebApplicationBuilder
var builder = WebApplication.CreateBuilder(args);

// Đăng ký các dịch vụ vào container

// Thêm dịch vụ Controllers và cấu hình tuỳ chọn cho việc chuyển đổi đối tượng JSON.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Đăng ký dịch vụ để khám phá các endpoint trong ứng dụng và sinh tài liệu API.
builder.Services.AddEndpointsApiExplorer();

// Đăng ký dịch vụ để tạo và cấu hình Swagger/OpenAPI cho API.
builder.Services.AddSwaggerGen();

// Cấu hình xác thực JWT (JSON Web Token)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
    };
});

// Cấu hình phân quyền (Authorization)

// Đăng ký dịch vụ HttpContextAccessor để truy cập thông tin về HTTP context trong các dịch vụ và middleware.
builder.Services.AddHttpContextAccessor();

// Cấu hình chính sách CORS cho phép bất kỳ origin nào có thể gửi yêu cầu có credentials (cookies, token) đến API.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .SetIsOriginAllowed(_ => true);
    });
});

// Xây dựng ứng dụng từ các dịch vụ đã đăng ký và cấu hình.
var app = builder.Build();

// Kiểm tra môi trường phát triển và kích hoạt việc sử dụng Swagger và SwaggerUI để tạo tài liệu và xem API.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Áp dụng chuyển hướng HTTPS cho các yêu cầu HTTP (nếu có).
app.UseHttpsRedirection();

// Áp dụng định tuyến các yêu cầu đến các action trong controller.
app.UseRouting();

// Áp dụng chính sách CORS cho ứng dụng. Đây là middleware CORS, nó được đặt trước các middleware xác thực và phân quyền.
app.UseCors();

// Áp dụng middleware xác thực, điều này cho phép server xác minh danh tính của người dùng dựa trên thông tin trong token.
app.UseAuthentication();

// Áp dụng middleware phân quyền, điều này cho phép server kiểm tra xem người dùng có quyền thực hiện action được yêu cầu hay không.
app.UseAuthorization();

// Định tuyến các yêu cầu đến các controller và action tương ứng.
app.MapControllers();

// Đưa ứng dụng vào chạy và xử lý các yêu cầu HTTP.
app.Run();
