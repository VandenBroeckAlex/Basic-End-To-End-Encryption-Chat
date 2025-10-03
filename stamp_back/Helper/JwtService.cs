using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace stamp_back.Helper
{
    public class JwtService
    {
        private string securekey = "bgetvqxYleb7LARoQyeEefLT7nJJZUEzr1xA8dral616OFJaD9m2jiMboQ4cs7o1mIdzpF3Ypo8RQA1j+bITatIAhlKYs/eaMnSVzli33J9lL2gToSqfwqRiax6XLl34fFPPJOnIEmZajokCk5z59Le8pFgt7OrV4KtFhK5oVg5xGYiDF18j6BE/CPfacJhN"; //change it and add to github secret or smthg
        public string generate(Guid userId)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securekey));
            var credential = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credential);


            DateTime JwtHeaderLifeTime = DateTime.UtcNow.AddHours(2);

            var payload = new JwtPayload(userId.ToString(), null, null, null, JwtHeaderLifeTime);
            var securityToken = new JwtSecurityToken(header,payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securekey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey (key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
        
    }


}
