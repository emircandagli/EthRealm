// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/token/ERC721/ERC721.sol";
import "@openzeppelin/contracts/access/Ownable.sol";

contract BeastNFT is ERC721, Ownable {
    uint256 public nextTokenId;

    struct Beast {
        uint256 hp;
        uint256 attack;
        uint256 defense;
        uint256 speed;
    }

    mapping(uint256 => Beast) public beasts;

    constructor() ERC721("BeastNFT", "BNFT") {}

    function mint(
        address to,
        uint256 hp,
        uint256 attack,
        uint256 defense,
        uint256 speed
    ) external onlyOwner {
        beasts[nextTokenId] = Beast(hp, attack, defense, speed);
        _safeMint(to, nextTokenId);
        nextTokenId++;
    }

    function getBeast(uint256 tokenId) external view returns (Beast memory) {
        return beasts[tokenId];
    }
}
