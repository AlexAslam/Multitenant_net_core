using AutoMapper;
using Moq;
using MultiTenantCore.Controllers;
using MultiTenantCore.DataModels.Mapper;
using MultiTenantCore.DataModels.Repository;
using MultiTenantCore.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace TestingMultiTenantCore
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            //var mockRepo = new Mock<ITenantRepository>();
            //mockRepo.Setup(repo => repo.getAllTenants()).Returns(Task.FromResult(GetTestSessions()));
            //var controller = new TenantController(mockRepo.Object,Mapper.Instance);
            //var results = await controller.Get();
            //var viewresult = Assert.IsType<TenantViewModel>(results);
            //var model = Assert.IsAssignableFrom<IEnumerable<TenantViewModel>>(viewresult.SubDomainName);
            //Assert.Equal(2, model);
        }
        private List<TenantViewModel> GetTestSessions()
        {
            var sessions = new List<TenantViewModel>();
            sessions.Add(new TenantViewModel()
            {
                SubDomainName = "alche1"
            });
            sessions.Add(new TenantViewModel()
            {
                SubDomainName = "alche2"
            });
            return sessions;
        }
    }
}
