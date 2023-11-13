using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using GenFu;
using Nest;
using FluentAssertions;
using UserPermissions.Api.Domain.Entities;
using UserPermissions.Api.Infrastructure.Data;
using UserPermissions.Api.Application.Queries;
using UserPermissions.Api.Infrastructure.Repositories;

namespace UserPermissions.Test
{
    public class GetPermissionQryHandlerTest
    {
        private IEnumerable<Permission> GetData()
        {
            A.Configure<Permission>()
                .Fill(x => x.Id, () => { return  1; })
                .Fill(x => x.EmployeeName, () => { return "Juan"; })
                .Fill(x => x.EmployeeLastname, () => { return "Perez"; })
                .Fill(x => x.PermissionDate, () => { return DateTime.Now; })
                .Fill(x => x.PermissionTypeId, () => { return 1; });

            var list = A.ListOf<Permission>(2);
            return list;
        }


        private Mock<PermissionContext> CreateContext()
        {
            var data = GetData().AsQueryable();
            var dbSet = new Mock<DbSet<Permission>>();
            
            dbSet.As<IQueryable<Permission>>().Setup(x => x.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<Permission>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Permission>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Permission>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            dbSet.As<IAsyncEnumerable<Permission>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<Permission>(data.GetEnumerator()));

            dbSet.As<IQueryable<Permission>>().Setup(x => x.Provider)
                .Returns(new AsyncQueryProvider<Permission>(data.Provider));

            var contexto = new Mock<PermissionContext>();
            contexto.Setup(z => z.Permissions).Returns(dbSet.Object);
            return contexto;
        }

        [Fact]
        public async Task Get_PermissionById_Should_BeOfType_GetPermissionByIdQryResponse()
        {
            //Arrange
            var mockContexto = CreateContext();
            var unitOfWork = new UnitOfWork(mockContexto.Object);
            var elasticClientMock = new Mock<IElasticClient>();
            var handler = new GetPermissionByIdQryHandler(unitOfWork, elasticClientMock.Object);

            //Act
            var qry = new GetPermissionbyIdQry { Id = 1 };
            var response = await handler.Handle(qry, new System.Threading.CancellationToken());

            //Assert
            response.Should().BeOfType<GetPermissionByIdQryResponse>();
        }

        [Fact]
        public async Task Get_PermissionById_Should_Work()
        {
            //Arrange
            var mockContexto = CreateContext();
            var unitOfWork = new UnitOfWork(mockContexto.Object);
            var elasticClientMock = new Mock<IElasticClient>();
            var handler = new GetPermissionByIdQryHandler(unitOfWork, elasticClientMock.Object);

            //Act
            var qry = new GetPermissionbyIdQry { Id = 1 };
            var response = await handler.Handle(qry, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Id == qry.Id);
        }
    }
}
