# Multitenant_net_core

it is in .net core 2.

This is just a demo app to help.
it is multitenant saas based application, which is api based application.
it has two dbcontexts, first is for base database in which only one table will create.
and second one is for saas based separte subdomain holders.

.net core 2

Add-Migration InitialCreate -context TenantContext

update-database -context TenantContext


Other Databases will create on creating the tenent entry