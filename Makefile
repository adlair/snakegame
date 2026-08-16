MONOXBUILD=/Library/Frameworks/Mono.framework/Commands/xbuild
MONOPROJECT=obj/
APKOUTPUT=bin/Release/
APK=apk/


all: prepare android

prepare:
	mkdir $(APK)

android: 
	$(MONOXBUILD) /p:Configuration=Release SnakeGame.csproj

clean: 
	-rm -rf bin/ obj/ $(APK)