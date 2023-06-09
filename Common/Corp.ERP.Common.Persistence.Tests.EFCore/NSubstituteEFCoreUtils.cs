﻿using Corp.ERP.Common.Persistence.Tests.EFCore.Utils;
using Microsoft.EntityFrameworkCore;

namespace Corp.ERP.Common.Persistence.Tests;

public class NSubstituteEFCoreUtils
{
    public static DbSet<TEntity> GetDbSetSubstitute<TEntity>(IList<TEntity> list, bool asyncQuerySupport = true) where TEntity : class
    {
        var mockSet = Substitute.For<DbSet<TEntity>, IQueryable<TEntity>, IAsyncEnumerable<TEntity>>();

        ((IQueryable<TEntity>)mockSet).Provider.Returns(asyncQuerySupport ? new TestDbAsyncQueryProvider<TEntity>(list.AsQueryable().Provider)
            : list.AsQueryable().Provider);
        ((IQueryable<TEntity>)mockSet).Expression.Returns(list.AsQueryable().Expression);
        ((IQueryable<TEntity>)mockSet).ElementType.Returns(list.AsQueryable().ElementType);
        ((IQueryable<TEntity>)mockSet).GetEnumerator().Returns(list.GetEnumerator());
        //((IEnumerable<TEntity>)mockSet).GetEnumerator().Returns(list.GetEnumerator());

        if (asyncQuerySupport)
        {
            ((IAsyncEnumerable<TEntity>)mockSet).GetAsyncEnumerator()
                .Returns(new TestDbAsyncEnumerator<TEntity>(list.GetEnumerator()));
        }

        return mockSet;
    }
}




