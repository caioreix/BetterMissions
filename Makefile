submodule-init:
	git submodule update --init

submodule-update:
	git submodule update --recursive --remote

choco-setup:
	choco install dotnet-6.0-sdk

restore-release:
	@dotnet restore /p:Configuration=Release

restore-test:
	@dotnet restore /p:Configuration=Test

build-test: restore-test
	@dotnet build . --configuration Test --no-restore

build-release: restore-release
	@dotnet build . --configuration Release --no-restore

test-test: restore-test
	@dotnet test . --configuration Test --no-restore

test-release: restore-release
	@dotnet test . --configuration Release --no-restore

source:
	@dotnet nuget update source github -u $(GH_USER) -p $(GH_TOKEN) --store-password-in-clear-text

clean:
	@if exist "./bin" rmdir /s /q "./bin"
	@if exist "./obj" rmdir /s /q "./obj"
	@if exist "./dist" rmdir /s /q "./dist"
	@if exist "./src/Utils/Vrising-Utils/bin" rmdir /s /q "./src/Utils/Vrising-Utils/bin"
	@if exist "./src/Utils/Vrising-Utils/obj" rmdir /s /q "./src/Utils/Vrising-Utils/obj"
	@if exist "./src/Utils/Vrising-Utils/dist" rmdir /s /q "./src/Utils/Vrising-Utils/dist"
	@if exist "./src/Utils/BepInEx-Utils/bin" rmdir /s /q "./src/Utils/BepInEx-Utils/bin"
	@if exist "./src/Utils/BepInEx-Utils/obj" rmdir /s /q "./src/Utils/BepInEx-Utils/obj"
	@if exist "./src/Utils/BepInEx-Utils/dist" rmdir /s /q "./src/Utils/BepInEx-Utils/dist"
