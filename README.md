Blockchain Investments [![Build Status](https://travis-ci.org/rafaelturon/blockchain-investments.svg?branch=master)](https://travis-ci.org/rafaelturon/blockchain-investments) [![Code Climate](https://codeclimate.com/github/rafaelturon/expense-point/badges/gpa.svg)](https://codeclimate.com/github/rafaelturon/blockchain-investments) [![built with gulp](https://img.shields.io/badge/gulp-project-eb4a4b.svg?logo=data%3Aimage%2Fpng%3Bbase64%2CiVBORw0KGgoAAAANSUhEUgAAAAYAAAAOCAMAAAA7QZ0XAAAABlBMVEUAAAD%2F%2F%2F%2Bl2Z%2FdAAAAAXRSTlMAQObYZgAAABdJREFUeAFjAAFGRjSSEQzwUgwQkjAFAAtaAD0Ls2nMAAAAAElFTkSuQmCC)](http://gulpjs.com/)
============

Blockchain Investments is an open-source project to increase control over your blockchain investments performance.

> **Live Demo App**: [http://blockchain-investments.herokuapp.com](http://blockchain-investments.herokuapp.com/)

# Description
This application will include:
* Safe token based authentication with BitId login using public key signature validation
* Ledger and journal double entry bookkeeping where entries are recorded by debiting one or more accounts and crediting another one or more accounts with the same total amount
* Automatic investment plan (recurring deposits)
* Monitor portfolio by risk (coins, bonds and stocks) and profile (from conservative to aggressive)

# Implementation

## Architecture
* CQRS/ES: [According to Martin Fowler](https://martinfowler.com/bliki/CQRS.html), CQRS stands for Command Query Responsibility Segregation. It's a pattern that I first heard described by [Greg Young](http://codebetter.com/gregyoung/).
![Command and Query Separation](https://martinfowler.com/bliki/images/cqrs/cqrs.png)

### Domain Driven Design (DDD) Bounded Contexts
* **Core**: *Optimal Asset Allocation*
* **Supporting**: *Saving and Investment*
* **Generic**: *Ledger and Pricing (external)*

### Technical Details
**Client Side** - Angular 2 TypeScript app packaged with WebPack and Bootstrap layout and styling:
* Angular 2 and TypeScript for client-side code. [Angular Universal] (https://github.com/aspnet/JavaScriptServices) for faster initial loading and improved SEO, your Angular 2 app is prerendered on the server. The resulting HTML is then transferred to the browser where a client-side copy of the app takes over;
* Webpack for building and bundling client-side resources. In development mode, there's no need to run the webpack build tool. Your client-side resources are dynamically built on demand. Updates are available as soon as you modify any file;
* Bootstrap for layout and styling. In production mode, development-time features are disabled, and the webpack build tool produces minified static CSS and JavaScript files.

**Server Side** - ASPNET Core backend with a restful API using Kestrel Web API Controllers:
* [ASP.NET Core](https://github.com/aspnet/Home) and C# for cross-platform server-side code
* CQRS/ES architecture using [CQRSLite](https://github.com/gautema/CQRSlite)
* Bitcoin support using [NBitcoin](https://github.com/MetacoSA/NBitcoin)
* Persistence layer using [MongoDB](https://github.com/mongodb/mongo)

## Build & development
Running `dotnet build` will build and `dotnet run` will present a preview.

## Testing
Running `dotnet test` will run the unit tests with xunit.

## Deployment
Running `dotnet publish` will run the deployment steps.
> Setting up `ASPNETCORE_ENVIRONMENT`, `MONGOLAB_URI` and `JWT_SECURITY_KEY` environment variables will be necessary in your deployment. [ASP.NET Core Environments](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments)