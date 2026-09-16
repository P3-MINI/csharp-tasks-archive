#include "rtweekend.h"
#include "hittable_list.h"
#include "material.h"
#include "sphere.h"
#include "color.h"
#include "camera.h"
#include <vector>
#include <cstring>
#include <memory>
#include <cstdint>

#define STB_IMAGE_WRITE_IMPLEMENTATION
#include "external/stb_image_write.h"

#if defined(_WIN32) || defined(_WIN64)
#define EXPORT __declspec(dllexport)
#else
#define EXPORT __attribute__((visibility("default")))
#endif

using std::make_shared;
using std::shared_ptr;

extern "C" {

    struct CameraConfig {
        double aspect_ratio;
        int    image_width;
        int    samples_per_pixel;
        int    max_depth;
        double vfov;
        point3 lookfrom;
        point3 lookat;
        vec3 vup;
        double defocus_angle;
        double focus_dist;
    };

    struct SceneHandle {
        hittable_list world;
    };

    EXPORT void* CreateScene() {
        return new SceneHandle();
    }

    EXPORT void DestroyScene(void* scene) {
        if (scene) {
            delete static_cast<SceneHandle*>(scene);
        }
    }

    EXPORT void* CreateLambertian(double r, double g, double b) {
        return new shared_ptr<material>(make_shared<lambertian>(color(r, g, b)));
    }

    EXPORT void* CreateMetal(double r, double g, double b, double fuzz) {
        return new shared_ptr<material>(make_shared<metal>(color(r, g, b), fuzz));
    }

    EXPORT void* CreateDielectric(double refraction_index) {
        return new shared_ptr<material>(make_shared<dielectric>(refraction_index));
    }

    EXPORT void DestroyMaterial(void* matPtr) {
        if (matPtr) {
            delete static_cast<shared_ptr<material>*>(matPtr);
        }
    }

    EXPORT void AddSphere(void* scenePtr, double cx, double cy, double cz, double radius, void* matPtr) {
        auto scene = static_cast<SceneHandle*>(scenePtr);
        auto mat = *static_cast<shared_ptr<material>*>(matPtr);
        scene->world.add(make_shared<sphere>(point3(cx, cy, cz), radius, mat));
    }

    typedef void (*RenderCallback)(int samples, uint8_t* buffer);

    EXPORT void RenderScene(
        void* scenePtr, 
        CameraConfig config,
        uint8_t* buffer,
        RenderCallback callback
    ) {
        auto scene = static_cast<SceneHandle*>(scenePtr);
        
        camera cam;
        cam.aspect_ratio      = config.aspect_ratio;
        cam.image_width       = config.image_width;
        cam.samples_per_pixel = config.samples_per_pixel;
        cam.max_depth         = config.max_depth;

        cam.vfov     = config.vfov;
        cam.lookfrom = point3(config.lookfrom[0], config.lookfrom[1], config.lookfrom[2]);
        cam.lookat   = point3(config.lookat[0], config.lookat[1], config.lookat[2]);
        cam.vup      = vec3(config.vup[0], config.vup[1], config.vup[2]);

        cam.defocus_angle = config.defocus_angle;
        cam.focus_dist    = config.focus_dist;

        cam.render(scene->world, buffer, callback);
    }

    EXPORT int SavePng(const char* filename, int width, int height, const uint8_t* buffer) {
        return stbi_write_png(filename, width, height, 4, buffer, width * 4);
    }
}
