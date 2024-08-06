use ObligatorioP3DB;
go
delete from [dbo].[Usuarios];
INSERT INTO [dbo].[Usuarios] (Email,Rol,Contrasena_ContrasenaEncriptada,Contrasena_ContrasenaNoEncriptada,NombreCompleto_Apellido,NombreCompleto_Nombre) VALUES
('Admin@gmail.com','admin','1c4572d4c3326b31034a63464ffbbc35f487d73a951a125eebaa8b699a232103','Admin.1','Admin','Admin'),
('Encargado@gmail.com','encargado','b7fda502a8b376fa74e672145f786836d95ac11bb89db9b7f000c85a2527001c','Encargado.1','Encargado','Encargado'),
('Usuarioa@gmail.com','admin','fe26abb71eb0d6d1fdaceece74f438d39b264a40da978c8a7186746841af88e1','UsuarioA.1','Usuarioa','Usuarioa'),
('Usuariob@gmail.com','admin','ec03256a93d16c3e92d9ce82093ab43e0ac079157bff22599bf38a1a47511097','UsuarioB.1','Usuariob','Usuariob'),
('Usuarioc@gmail.com','admin','acfcf89b6e1c87e5e0334a667dfaa661aa03d88a3105c6a2680a61d44cba4d14','UsuarioC.1','Usuarioc','Usuarioc'),
('Usuariod@gmail.com','admin','d9bc75be30c690c2b484cd048b9d5a9e36cea0aeb911d8d2d5451529d3bb609e','UsuarioD.1','Usuariod','Usuariod'),
('Usuarioe@gmail.com','admin','06ccdf93027aeabc98155010c1577024da16866952b562a64df4aac6ca6085d2','UsuarioE.1','Usuarioe','Usuarioe'),
('Usuariof@gmail.com','admin','3287b0a17e5a8c7e22f2a88f530883635ab52311a903ac02f207d7fdd300a456','UsuarioF.1','Usuariof','Usuariof'),
('Usuariog@gmail.com','admin','68088871067d1521a93b094de967d7eb887d6533c8212f791f47b87c5e6ccc7a','UsuarioG.1','Usuariog','Usuariog'),
('Usuarioh@gmail.com','admin','dbeec5af8745f3f226b2b299ce60a0ca2c980a0cc51a0f396a3c1cf9ca03ccdf','UsuarioH.1','Usuarioh','Usuarioh'),
('Usuarioi@gmail.com','admin','d28c99ba52284d88b7a1dcf319ebf550663b0ba2012e9b3ce8d6ea0b75f2d397','UsuarioI.1','Usuarioi','Usuarioi'),
('Usuarioj@gmail.com','admin','39d90289a877df33edb38710001e1d3cdc9e7707e77ad598f23555be919e5039','UsuarioJ.1','Usuarioj','Usuarioj'),
('Usuariok@gmail.com', 'encargado', 'fdc2e0957483474c1266f6d863ff4564b778d5528ba4393f6f087c617b845fc6', 'UsuarioK.1', 'Usuariok', 'Usuariok'),
('Usuariol@gmail.com', 'encargado', 'aa0c9f7e941d979bec7df58dd7dab8dfb588c52c81a9f4ff7daf4846cf50419f', 'UsuarioL.1', 'Usuariol', 'Usuariol'),
('Usuariom@gmail.com', 'encargado', '6c6bdb84c7c840d53a5f3f54db4e06c9503e89b3bff917c619fff4c3a3085972', 'UsuarioM.1', 'Usuariom', 'Usuariom'),
('Usuarion@gmail.com', 'encargado', '9bffeadfb1731a930315b651dd21f4e2959bdf0c73693c37d0a86e4387ddc46b', 'UsuarioN.1', 'Usuarion', 'Usuarion'),
('Usuarioo@gmail.com', 'encargado', '63785c36016c2b86073ade14e7137541b9e9143330ea425999aa69582c2a2cc0', 'UsuarioO.1', 'Usuarioo', 'Usuarioo'),
('Usuariop@gmail.com', 'encargado', '2cc4745b648911e3ec9ba7850d092e416a76511d6501992c7e5148a7d921c54e', 'UsuarioP.1', 'Usuariop', 'Usuariop'),
('Usuarioq@gmail.com', 'encargado', '6f15cf99170f719ccbecb0047c278e74b9c34f9a112d82094a9d91b6b56da79d', 'UsuarioQ.1', 'Usuarioq', 'Usuarioq'),
('Usuarior@gmail.com', 'encargado', 'd48c452ad915bce71372b54a50d7e337ecb7b760dd115ac216f57c8073b49654', 'UsuarioR.1', 'Usuarior', 'Usuarior'),
('Usuarios@gmail.com', 'encargado', '2f165fd3448fa85343382f4e7f031f7b1616fae703ff915270515bae6086be22', 'UsuarioS.1', 'Usuarios', 'Usuarios'),
('Usuariot@gmail.com', 'encargado', '2de15f75b846fac4ff55d8442c3b12fddc8dcad283644fd5c2841fc6dd7e42d1', 'UsuarioT.1', 'Usuariot', 'Usuariot');
