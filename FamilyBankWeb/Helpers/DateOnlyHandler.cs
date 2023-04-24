using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers;
public class DateOnlyHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly date)
    {
        parameter.DbType = DbType.DateTime;
        parameter.Value = date.ToDateTime(new TimeOnly(0, 0));
    }

    public override DateOnly Parse(object value)
        => DateOnly.FromDateTime((DateTime)value);
}