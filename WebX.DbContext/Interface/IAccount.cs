using System;
using System.Collections.Generic;
using System.Text;
using WebX.MODEL;

namespace WebX.DbAccess.Interface
{
    public interface IAccount
    {
        // 插入数据
        bool CreateAccount(AccountMD account);

        //取全部数据
        IEnumerable<AccountMD> GetAccounts();

        //取某LineNO记录
        AccountMD GetAccountByLineNo(int LineNo);

        //根据LineNo更新整条记录
        bool UpdateAccount(AccountMD account);

        //根据LineNo更新名称
        bool UpdateNameByLineNo(int LineNo, string name);

        //根据id删除记录
        bool DeleteAccountByLineNo(int LineNo);

    }
}
