# DiuDiu
简单实现的服务注册，检查，服务网关

网关服务端  ,具体参考demo.service

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddControllers();


            services.AddDiuDiu(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDiuDiu();

        }


需要注册的服务端  
第一次启动需要向DiuDiuService注册服务 ,具体参考demo.client

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers();
            services.AddDiuDiu(a=> {
                a.Address = Configuration.GetValue<string>("DiuDiuUrl");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/helth");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



        var client = services.GetRequiredService<IDiuDiuClient>();
        DiuDiuService service = new DiuDiuService()
        {
            Host="localhost",
            Port=5004,
            Name="UserApi",
            ID="xxxxxxxx",
            Check=new DiuDiuServiceCheck() {
                Address= "http://localhost:5004/helth",
                ErrorTimes=5,
                Interval=10,
                StartCheckTime=5,
                TimeOut=5
            }
        };
        client.RegisterService(service).Wait();