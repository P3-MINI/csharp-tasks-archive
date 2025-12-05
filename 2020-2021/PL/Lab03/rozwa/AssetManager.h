#ifndef ADDET_MANAGER_HEADER
#define ADDET_MANAGER_HEADER

#include <string>

struct Asset
{
    std::string GetName() { return this->m_AssetName; }

    Asset(const std::string& assetName) : m_AssetName(assetName) { }

protected: std::string m_AssetName;
};

struct SoundAsset : public Asset
{
    SoundAsset(const std::string& assetName) : Asset(assetName) { }
};

struct MeshAsset : public Asset
{
    MeshAsset(const std::string& assetName) : Asset(assetName) { }
};

#endif /* ADDET_MANAGER_HEADER */
