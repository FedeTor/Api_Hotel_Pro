using Common.HashMethods;
using Dapper;
using DapperBD;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Common.Enumerables.EnumerablesDB;

namespace Common.Middleware
{
    public class ServiceToken : IServiceToken
    {
        private readonly DapperContext _context;

        public ServiceToken(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> CancelarTokenAsync(string token)
        {
            // se usa para el logout
            try
            {
                var hashSesion = Hashes.HashearToken(token);
                var query = @"INSERT INTO TokenCancelados (Hash) VALUES (@hash)";
                using(var connection = _context.CreateConnection())
                {
                    var insertToken = await connection.ExecuteAsync(query, new
                    {
                        hash = hashSesion,
                    });

                    if(insertToken == (int)EnumInsert.Insert_Ok)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ValidarTokenAsync(string token)
        {
            try
            {                
                var hashSesion = Hashes.HashearToken(token);
                var query = @"SELECT * FROM TokenCancelados WHERE Hash = @hash";
                using(var connection = _context.CreateConnection())
                {
                    var tokenRepetido = (await connection.QueryAsync<CancelarToken>(query, new { hash = hashSesion })).FirstOrDefault();

                    if (tokenRepetido == null)
                    {
                        return true; // el token no se encuentra en la blacklist, ergo es un token válido
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
