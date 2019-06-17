using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebX.DbAccess;
using WebX.DbAccess.Interface;
using WebX.MODEL;
using System.Data.SqlClient;
using System.Reflection;

namespace WebX.BLL
{
    public class AccountBLL : IAccount
    {
        private readonly MySqlContext _context;
        public AccountBLL(MySqlContext context)
        {
            _context = context;
        }
        

        public  bool CreateAccount(AccountMD account)
        {
            _context.AccountMD.
            _context.AccountMD.Add(account);
            
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<AccountMD> GetAccounts()
        {

            //_context.AccountMD;
            return _context.AccountMD.ToList();
        }

        public AccountMD GetAccountByLineNo(int LineNo)
        {
            return _context.AccountMD.SingleOrDefault(u => u.LineNo == LineNo);
        }

        public bool UpdateAccount(AccountMD account)
        {
            _context.AccountMD.Update(account);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateNameByLineNo(int LineNo, string name)
        {
            var state = false;
            var user = _context.AccountMD.SingleOrDefault(u => u.LineNo == LineNo);
            if (user != null)
            {
                user.UserNickname = name;
                state = _context.SaveChanges() > 0;
            }
            return state;
        }

        public bool DeleteAccountByLineNo(int LineNo)
        {
            var user = _context.AccountMD.SingleOrDefault(u => u.LineNo == LineNo);
            _context.AccountMD.Remove(user);
            return _context.SaveChanges() > 0;
        }
    }
}
